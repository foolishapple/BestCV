using BestCV.API.Utilities;
using BestCV.Application.Models.Candidates;
using BestCV.Application.Models.Candidates;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using BestCV.Domain.Aggregates.Candidate;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Text;
using BestCV.Application.Models.CandidateJobSuggestionSetting;
using BestCV.Application.Models.Employer;
using BestCV.Domain.Constants;

namespace BestCV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : BaseController
    {
        private readonly ICandidateService service;
        private readonly ILogger<CandidateController> logger;
        private readonly IConfiguration configuration;
        public CandidateController (ICandidateService _service, ILoggerFactory loggerFactory, IConfiguration _configuration)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CandidateController>();
            configuration = _configuration;
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] long id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get candidate by id: {id}");
                return BadRequest();
            }
        }

        [HttpGet("candidate-detail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CandidateDetail()
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();
                var response = await service.ListCandidateDetailById(candidateId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get candidate ");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> Signup([FromBody] SignupCandidateDTO signup)
        {
            try
            {
                var (result, emailMessage) = await service.CandidateSignup(signup);
                //if (emailMessage != null)
                //{
                //    var urlAPI = configuration["SectionUrls:CandidateSendConfirmEmailAPI"];
                //    //Thêm giá trị cho trường path
                //    emailMessage.TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "CandidateConfirmEmail.html");
                //    //Send email
                //    using var httpClient = new HttpClient();
                //    await httpClient.SendRequestAsync<EmailMessage<CandidateConfirmEmailBody>, string>(urlAPI, emailMessage);
                //}
                return Ok(result);
            }catch(Exception ex)
            {
                logger.LogError(ex, "Đăng ký tài khoản thất bại!");
                return BadRequest();
            }
        }


 
        [HttpPost]
        [Route("send-email-from-server")]
        public async Task<IActionResult> SendEmailFromServer([FromBody] EmailMessage<CandidateConfirmEmailBody> emailMessage) {
            try
            {
                var urlAPI = configuration["SectionUrls:CandidateSendConfirmEmailAPI"];
                //Thêm giá trị cho trường path
                emailMessage.TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "CandidateConfirmEmail.html");
                //Send email
                using var httpClient = new HttpClient();
                await httpClient.SendRequestAsync<EmailMessage<CandidateConfirmEmailBody>, string>(urlAPI, emailMessage);

                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Đăng ký tài khoản thất bại!");
                return BadRequest();
            }
        }

        [HttpPut("verify-email-code")]
        public async Task<IActionResult> VerifyEmailCode([FromBody] ConfirmEmailCandidateDTO obj)
        {
            try
            {
                var result = await service.CheckVerifyCode(obj.Value, obj.Hash);
                if(result.IsSucceeded && result.Status == 200)
                {
                    var candidate = result.Resources as CandidateMeta;

                    await service.ActivateAccount(candidate.Name);
                    return Ok(BestCVResponse.Success("Chúc mừng bạn đã xác thực tài khoản thành công, hãy đăng nhập để sử dụng tính năng của ứng viên"));
                }
                else if(!result.IsSucceeded && result.Status == 400)
                {
                    logger.LogError($"Confirm link is expired");
                    return Ok(BestCVResponse.Error("Link xác thực của bạn đã hết hạn, hãy nhấn nút gửi lại để nhận email xác thực mới"));
                }
                else
                {
                    logger.LogError($"Confirm link with wrong code & hash");
                    return Ok(BestCVResponse.NotFound("Link xác thực của bạn không đúng, vui lòng thử lại link khác", obj));
                }
            }catch(Exception e)
            {
                logger.LogError(e, "User verify email doesn't success");
                return BadRequest();
            }
        }

        [HttpPost("re-send-email")]
        public async Task<IActionResult> ReSendConfirmEmail([FromBody] ConfirmEmailCandidateDTO obj)
        {
            try
            {
                var candidateMeta = await service.GetEmailByVerifyCode(obj);
                if(candidateMeta != null)
                {
                    var (result, emailMessage) = await service.ReSendEmail(candidateMeta.Name);
                    if(emailMessage!= null)
                    {
                        if (!result.IsSucceeded)
                        {
                            return Ok(BestCVResponse.Error("Bạn đã yêu cầu gửi email quá 5 lần 1 ngày, vui lòng đợi đến ngày mai để gửi yêu cầu mới."));
                        }
                        else
                        {
                            var verifyCodeConfirmEmailApi = configuration["SectionUrls:CandidateSendConfirmEmailAPI"];

                            emailMessage.TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "CandidateConfirmEmail.html");
                            using var httpClient = new HttpClient();
                            await httpClient.SendRequestAsync<EmailMessage<CandidateConfirmEmailBody>, string>(verifyCodeConfirmEmailApi, emailMessage);

                            return Ok(BestCVResponse.Success());
                        }
                    }
                    else
                    {
                        return Ok(BestCVResponse.BadRequest(obj));
                    }

                }

                return Ok();
                
            }
            catch(Exception e)
            {
                logger.LogError(e, $"Failed to re-send mail to email: {obj}");
                return BadRequest();
            }
        }
    
        [HttpPut("update-password-candidate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdatePasswordCandidate([FromBody] ChangePasswordDTO obj)
        {
            try
            {
                var userId = this.GetLoggedInUserId();              
                var response = await service.UpdatePasswordCandidate(obj,userId);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to update password to Candidate account id : {obj.Id}");
                return BadRequest();
            }
        }

        [HttpPut("update-noti-email")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateNotiEmailCandidate([FromBody] SettingNotiEmailDTO obj)
        {
            try
            {               
                obj.Id = this.GetLoggedInUserId();
                var response = await service.UpdateNotiEmailCandidate(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to update Noti Email to Candidate account id : {obj.Id}");
                return BadRequest();
            }
        }

    
  
        [HttpPut("update-profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateProfileCandidate([FromBody] UpdateProfileCandidateDTO obj)
        {
            try
            {
                obj.Id = this.GetLoggedInUserId();
                var response = await service.UpdateProfileCandidate(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to update profile to Candidate account id : {obj.Id}");
                return BadRequest();
            }
        }

        [HttpPut("job-suggestion-setting")]
        public async Task<IActionResult> JobSuggestionSetting([FromBody] UpdateCandidateJobSuggetionSettingDTO obj)
        {
            try
            {
                obj.Id = this.GetLoggedInUserId();
                var response = await service.UpdateCandidateJobSuggestionSetting(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to update job suggestion setting to Candidate  : {obj.Id}");
                return BadRequest();
            }
        }



        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SigninCandidateDTO model)
        {
            try
            {
                var res = await service.SignIn(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Candidate failed to sign in");
                return BadRequest();
            }
        }

        [HttpPost("list-candidate-aggregates")]
        public async  Task<IActionResult> ListCandidateAgrregates(CandidateDTParameters parameters)
        {
            try
            {
                return Json(await service.ListCandidateAggregates(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load list candidate aggregates");
                return BadRequest();
            }
        }
   
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
                logger.LogError(ex, "Fail to quick activated async");
                return BadRequest();
            }
        }
     
        [HttpPost("export-excel")]
        public async Task<IActionResult> ExportExcel(List<CandidateAggregates> data)
        {
            try
            {
                var response = await service.ExportExcel(data); 
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to export excel");
                return BadRequest();
            }
        }

     
        [HttpPost("sign-in-google")]
        public async Task<IActionResult> SignInGoogle([FromBody] SignInWithSocialNetworkDTO obj)
        {
            try
            {
                var res = await service.SignInWithGoole(obj);
                return Ok(res);
            }catch(Exception ex)
            {
                logger.LogError(ex, "Candidate failed to sign with google");
                return BadRequest();
            }
        }

        [HttpPost("sign-in-facebook")]
        public async Task<IActionResult> SignInFacebook([FromBody] SignInWithSocialNetworkDTO obj)
        {
            try
            {
                var res = await service.SignInWithFacebook(obj);
                return Ok(res);
            }catch(Exception ex)
            {
                logger.LogError(ex, "Candidate failed to sign with facebook");
                return BadRequest();
            }
        }
          
    
        [HttpGet]
        [Route("download-excel")]
        public IActionResult DownloadExcel(string fileGuid, string fileName)
        {
            try
            {
                var data = service.DownloadExcel(fileGuid, fileName);
                if(data != null)
                {
                    return File(data, "application/vnd.ms-excel", fileName);
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to download Excel");
                return BadRequest(ex.Message);
            }
        }

   
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            try
            {
                var response = await service.ChangePasswordAdminAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to change passwod");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-in-linkedIn")]
        public async Task<IActionResult> SignInLinkedIn([FromBody] SignInWithSocialNetworkDTO obj)
        {
            try
            {
                var res = await service.SignInWithLinkedIn(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Candidate failed to sign with LinkedIn");
                return BadRequest();
            }
        }

 
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO obj)
        {
            try
            {
                var candidate = await service.GetByEmailAsync(obj.Email);
                var isEmailActive = await service.CheckEmailIsActive(obj.Email);
                // check email đã kích hoạt chưa ?
                if (candidate == null && !isEmailActive)
                {
                    return Ok(BestCVResponse.Error(420, "Email của bạn chưa được kích hoạt"));
                }
                // check email đã được gửi bao lần
                var countRequest = await service.CountResetPassword(candidate.Id);
                if (countRequest >= CandidateConstants.FORGOT_PASSWORD_SENT_EMAIL_LIMIT)
                {
                    return Ok(BestCVResponse.BadRequest("Bạn đã gửi mail quá " + CandidateConstants.FORGOT_PASSWORD_SENT_EMAIL_LIMIT + " lần vui lòng gửi lại trong " + CandidateConstants.FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT + " giờ."));
                }

                var (result, emailMessage) = await service.ForgotPassword(obj.Email);
                if (emailMessage != null)
                {
                    var urlAPI = configuration["SectionUrls:CandidateSendForgotPasswordEmaillAPI"];
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
                logger.LogError(ex, "Failed to send email reset password to candidate ");
                return BadRequest();
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> RestPassword([FromBody] ResetPasswordCandidateDTO obj)
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
                logger.LogError(ex, "Failed to reset password to candidate ");
                return BadRequest(obj);
            }
        }

        [HttpPost("list-candidate-find-cv")]
        public async Task<IActionResult> ListCandidateFindCV(FindCandidateParameters parameters)
        {
            try
            {
                parameters.EmployerId = this.GetLoggedInUserId();
                return Json(await service.FindCandidateAgrregates(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load list candidate find cv");
                return BadRequest();
            }
        }
    }
}
