using BestCV.API.Utilities;
using BestCV.Application.Models.Employer;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BestCV.Application.Services.Implement;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using BestCV.Application.Models.Candidates;
using BestCV.Domain.Aggregates.Candidate;
using BestCV.Domain.Aggregates.Employer;
using BestCV.Domain.Constants;

namespace BestCV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : BaseController
    {
        private readonly IEmployerService service;
        private readonly ILogger<EmployerController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public EmployerController(IEmployerService _service, ILoggerFactory loggerFactory, ITokenService tokenService, IConfiguration configuration)
        {
            service = _service;
            _logger = loggerFactory.CreateLogger<EmployerController>();
            _tokenService = tokenService;
            _configuration = configuration;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        #region Additional Resources

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 28/07/2023
        /// Description: Chức năng đăng nhập nhà tuyển dụng
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInEmployerDTO obj)
        {
            try
            {
                var response = await service.SignInAsync(obj);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to sign in employer");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 28/07/2023
        /// Description: Chức năng cập nhật mật khẩu nhà tuyển dụng
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordEmployerDTO obj)
        {
            try
            {
                var userId = this.GetLoggedInUserId();
                //  var userId = 1049;
                var response = await service.ChangePassword(userId, obj);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update password to employer account ");
                return BadRequest();
            }
        }


        /// <summary>
        /// author: truongthieuhuyen
        /// created: 26-07-2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Gửi request đến api send email bên ScheduledJobController & trả về response</returns>
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] EmployerSignUpDTO obj)
        {
            try
            {
                var (result, emailMessage) = await service.SignUp(obj);
                if (emailMessage != null)
                {
                    try
                    {
                        var urlAPI = _configuration["SectionUrls:EmployerSendConfirmEmailAPI"];
                        // thêm giá trị cho trường path 
                        emailMessage.TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "EmployerConfirmEmail.html");
                        // send email
                        using (HttpClient httpClient = new())
                        {
                            var res = await httpClient.SendRequestAsync<EmailMessage<EmployerConfirmEmailBody>, string>(urlAPI, emailMessage);
                            _logger.LogInformation(res);
                        };

                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"SEND_EMAIL_FAILED");
                    }

                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed for employer to sign up: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 31.07.2023
        /// </summary>
        /// <returns></returns>
        [HttpPost("re-send-email")]
        public async Task<IActionResult> ReSendEmailConfirm([FromBody] ConfirmEmailEmployerDTO obj)
        {
            try
            {
                var empMeta = await service.GetEmailByVerifyCode(obj);
                if (empMeta != null)
                {
                    var (result, emailMessage) = await service.ReSendEmail(empMeta.Name);
                    if (emailMessage != null)
                    {
                        // TH1: quá giới hạn 5 lần 1 ngày
                        if (!result.IsSucceeded)
                        {
                            return Ok(BestCVResponse.Error("Bạn đã yêu cầu gửi email quá 5 lần 1 ngày, vui lòng đợi đến ngày mai để gửi yêu cầu mới."));
                        }
                        // TH2: chưa quá giới hạn thì gửi email
                        else
                        {
                            var urlAPI = _configuration["SectionUrls:EmployerSendConfirmEmailAPI"];
                            // thêm giá trị cho trường path 
                            emailMessage.TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "EmployerConfirmEmail.html");
                            // send email
                            using var httpClient = new HttpClient();
                            await httpClient.SendRequestAsync<EmailMessage<EmployerConfirmEmailBody>, string>(urlAPI, emailMessage);

                            return Ok(BestCVResponse.Success());
                        }
                    }
                    // không tìm thấy cặp value, hash
                    else
                    {
                        return Ok(BestCVResponse.BadRequest(obj));
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to re-send mail to email: {obj}");
                return BadRequest();
            }
        }



        /// <summary>
        /// author: truongthieuhuyen
        /// created: 29.07.2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("verify-email-code")]
        public async Task<IActionResult> VerifyEmailCode([FromBody] ConfirmEmailEmployerDTO obj)
        {
            try
            {
                var result = await service.CheckVerifyCode(obj.Value, obj.Hash);
                if (result.IsSucceeded && result.Status == 200)
                {
                    var meta = result.Resources as EmployerMeta;

                    // activate account
                    await service.ActivateAccount(meta.Name);
                    return Ok(BestCVResponse.Success("Chúc mừng bạn đã xác thực tài khoản thành công, hãy đăng nhập để sử dụng tính năng của nhà tuyển dụng"));
                }
                else if (!result.IsSucceeded && result.Status == 400)
                {
                    _logger.LogError($"Confirm link is expired");
                    return Ok(BestCVResponse.Error("Link xác thực của bạn đã hết hạn, hãy nhấn nút gửi lại để nhận email xác thực mới"));
                }
                else
                {
                    _logger.LogError($"Confirm link with wrong code & hash");
                    return Ok(BestCVResponse.NotFound("Link xác thực của bạn không đúng, vui lòng thử lại link khác", obj));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "User verify email doesn't success.");
                return BadRequest(BestCVResponse.BadRequest(e));
            }
        }

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 04/08/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost("list-employer-aggregates")]
        public async Task<IActionResult> ListEmployerAgrregates(DTParameters parameters)
        {
            try
            {
                return Json(await service.ListEmployerAggregates(parameters));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to load list candidate aggregates");
                return BadRequest();
            }
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: Chức năng gửi email khi quên mật khẩu
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO obj)
        {
            try
            {
                var employer = await service.GetByEmailAsync(obj.Email);
                var isEmailActive = await service.CheckEmailIsActive(obj.Email);
                // check email đã kích hoạt chưa ?
                if (employer == null && !isEmailActive)
                {
                    return Ok(BestCVResponse.Error(420, "Email của bạn chưa được kích hoạt"));
                }
                // check email đã được gửi bao lần
                var countRequest = await service.CountResetPassword(employer.Id);
                if (countRequest >= EmployerMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_LIMIT)
                {
                    return Ok(BestCVResponse.BadRequest("Bạn đã gửi mail quá " + EmployerMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_LIMIT + " lần vui lòng gửi lại trong " + EmployerMetaConstants.CONFIRM_EMAIL_EXPIRY_TIME + " giờ."));
                }

                var (result, emailMessage) = await service.ForgotPassword(obj.Email);
                if (emailMessage != null)
                {
                    var urlAPI = _configuration["SectionUrls:EmployerSendForgotPasswordEmaillAPI"];
                    // thêm giá trị cho trường path 
                    emailMessage.TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "ForgotPasswordEmail.html");
                    // send email
                    using var httpClient = new HttpClient();
                    await httpClient.SendRequestAsync<EmailMessage<ForgotPasswordEmailBody>, string>(urlAPI, emailMessage);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email reset password to employer ");
                return BadRequest();
            }
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/8/2023
        /// Description: Chức năng thay đổi mật khẩu từ link email quên mật khẩu
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> RestPassword([FromBody] ResetPasswordEmployerDTO obj)
        {
            try
            {
                var checkKeyValid = await service.CheckKeyValid(obj.Code, obj.Hash);
                if (!checkKeyValid)
                {
                    obj.KeyUpToDate = false;
                    var bestCVResponse = BestCVResponse.NotFound("Key đã hết hạn", obj);
                    return Ok(bestCVResponse);
                }
                var response = await service.ResetNewPassword(obj.Code, obj.Hash, obj.Password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reset password to employer ");
                return BadRequest(obj);
            }
        }

        #endregion


        #region CRUD     
        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 28/07/2023
        /// Description : Detail by id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update-profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerDTO obj)
        {
            var employerId = this.GetLoggedInUserId();
            try
            {
                obj.Id = employerId;
                var res = await service.UpdateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update employer");
                return BadRequest();
            }
        }
        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 28/07/2023
        /// Description : Detail by id
        /// </summary>
        /// <param"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Detail()
        {
            var employerId = this.GetLoggedInUserId();
            try
            {
                var response = await service.GetByIdAsync(employerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get detail employer by id: {employerId}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhNd 
        /// CreatedTime: 04/08/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail-admin/{id}")]
        public async Task<IActionResult> DetailAdmin([Required] long id)
        {
            try
            {
                var response = await service.AdminDetailAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get detail employer by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 04/08/2023
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("change-password-admin")]
        public async Task<IActionResult> ChangePasswordAdmin([FromBody] ChangePasswordDTO model)
        {
            try
            {
                var response = await service.ChangePasswordAdminAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to change passwod");
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 04/08/2023
        /// </summary>
        /// <param name="id">EmployerId</param>
        /// <returns></returns>
        [HttpPut("quick-activated")]
        public async Task<IActionResult> QuickActivated([Required] long id)
        {
            try
            {
                var response = await service.QuickActivatedAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to quick activated async");
                return BadRequest();
            }
        }
        #endregion
    }
}
