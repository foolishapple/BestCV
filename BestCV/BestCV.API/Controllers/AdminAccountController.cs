using BestCV.API.Utilities;
using BestCV.Application.Models.AdminAccounts;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using BestCV.Application.Models.Employer;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;

namespace BestCV.API.Controllers
{
    [Route("api/admin-account")]
    [ApiController]
    public class AdminAccountController : BaseController
    {
        private readonly IAdminAccountService _service;
        private readonly ILogger<AdminAccountController> _logger;
        private readonly IConfiguration _configuration;
        public AdminAccountController(IAdminAccountService service, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<AdminAccountController>();
            _configuration = configuration;
        }

        #region CRUD

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required(ErrorMessage = "Mã tài khoản quản trị viên không được để trống")] long id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get admin account by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertAdminAccountDTO obj) {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed when insert new admin account");
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAdminAccountDTO obj)
        {
            try
            {
                var response = await _service.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed when insert new admin account with id: {obj.Id}");
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Mã tài khoản quản trị viên không được để trống")]long id)
        {
            try
            {
                var response = await _service.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to delete admin account by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add-many")]
        public async Task<IActionResult> AddMany([FromBody] IEnumerable<InsertAdminAccountDTO> objs)
        {
            try
            {
                var response = await _service.CreateListAsync(objs);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add many admin account");
                return BadRequest();
            }
        }

        [HttpPut("update-many")]
        public async Task<IActionResult> UpdateMany([FromBody] IEnumerable<UpdateAdminAccountDTO> objs)
        {
            try
            {
                var response = await _service.UpdateListAsync(objs);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update list admin account");
                return BadRequest();
            }
        }

        [HttpGet("list-all")]
        public async Task<IActionResult> ListAll()
        {
            try
            {
                var response = await _service.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all admin account");
                throw;
            }
        }

        [HttpPost("update-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdatePassWord([FromBody] ChangePasswordAdminAccountDTO obj)
        {
            try
            {
                obj.Id = this.GetLoggedInUserId();
                var response = await _service.UpdatePassword(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to update password to admin account id:{obj.Id}");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInAdminAccountDTO obj)
        {
            try
            {
                var response = await _service.SignIn(obj);
                if (response.IsSucceeded)
                {
                    HttpContext.Response.Cookies.Append("Authorization", JwtBearerDefaults.AuthenticationScheme + " " + ((SignInResponse)response.Resources).AccessToken);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to sign in");
                return BadRequest();
            }
        }

        [HttpGet("user-info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UserInfo()
        {
            try
            {
                long userId = this.GetLoggedInUserId();
                var response = await _service.GetByIdAsync(userId);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get admin account infomation");
                return BadRequest();
            }
        }

 
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] FotgotPasswordAdminAccountDTO obj)
        {
            try
            {
                var adminAccount = await _service.GetByEmailAsync(obj.Email);
                if (adminAccount != null)
                {
                    var isEmailActive = await _service.CheckEmailIsActive(obj.Email);
                    var isAdminAccount = await _service.IsAdminRole(adminAccount.Id);
                    // check email đã kích hoạt chưa ?
                    if (!isEmailActive)
                    {
                        return Ok(BestCVResponse.Error(420, "Email của bạn chưa được kích hoạt"));
                    }
                    // check email đã được gửi bao lần
                    var countRequest = await _service.CountResetPassword(adminAccount.Id);
                    if (countRequest >= AdminAccountMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_LIMIT)
                    {
                        return Ok(BestCVResponse.BadRequest("Bạn đã gửi mail quá " + AdminAccountMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_LIMIT + " lần vui lòng gửi lại trong " + AdminAccountMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT + " giờ."));
                    }
                    if (!isAdminAccount)
                    {
                        return Ok(BestCVResponse.Error(410, "Tài khoản Admin không tồn tại"));
                    }
                    var (result, emailMessage) = await _service.ForgotPassword(obj.Email);
                    if (emailMessage != null)
                    {
                        var urlAPI = _configuration["SectionUrls:AdminSendForgotPasswordEmaillAPI"];
                        // thêm giá trị cho trường path 
                        emailMessage.TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "ForgotPasswordEmail.html");
                        // send email
                        using var httpClient = new HttpClient();
                        await httpClient.SendRequestAsync<EmailMessage<ForgotPasswordEmailBody>, string>(urlAPI, emailMessage);
                    }
                    return Ok(result);
                }
                else
                {
                    var bestCVResponse = BestCVResponse.NotFound("Không tìm thấy Email", adminAccount);
                    return NotFound(bestCVResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email reset password to employer ");
                return BadRequest();
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> RestPassword([FromBody] ResetPasswordAdminAccountDTO obj)
        {
            try
            {
                var checkKeyValid = await _service.CheckKeyValid(obj.Code, obj.Hash);
                if (!checkKeyValid)
                {
                    obj.KeyUpToDate = false;
                    var bestCVResponse = BestCVResponse.NotFound("Key đã hết hạn", obj);
                    return Ok(bestCVResponse);
                }
                var response = await _service.ResetNewPassword(obj.Code, obj.Hash, obj.Password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reset password to employer ");
                return BadRequest(obj);
            }
        }
        #endregion
    }
}
