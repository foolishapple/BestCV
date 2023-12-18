using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BestCV.ScheduledJob.Controllers
{
    [Route("api/scheduled-jobs")]
    [ApiController]
    public class ScheduledJobController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly IScheduledJobService scheduledJobService;
        private readonly ILogger<ScheduledJobController> logger;


        public ScheduledJobController(IEmailService _emailService, IScheduledJobService _scheduledJobService, ILoggerFactory loggerFactory)
        {
            emailService = _emailService;
            scheduledJobService = _scheduledJobService;
            logger = loggerFactory.CreateLogger<ScheduledJobController>();
        }

        [HttpPost("send-mail")]
        public IActionResult SendEmail()
        {

            try
            {
                var message = new EmailMessage<EmployerConfirmEmailBody>()
                {
                    ToEmails = new List<string> { "minhtlse05236@fpt.edu.vn" },
                    CcEmails = new List<string> { },
                    BccEmails = new List<string> { },
                    Subject = "Xác thực tài khoản đăng ký hệ thống ELearning",
                    TemplatePath = "D:\\Dion\\Project\\Jobi\\Jobi.Web\\Utilities\\EmailTemplates\\Register.html",
                    Model = new EmployerConfirmEmailBody() { Fullname = "MinhTL", Otp = "123456", ActiveLink = $"test", Time = DateTime.Now.Year }
                };
                var jobId = scheduledJobService.Enqueue(() => emailService.SendEmail(message));
                logger.LogInformation($"Sent email to {string.Join(", ", message.ToEmails)} with subject {message.Subject} - Job Id: {jobId}");
                return Ok(jobId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email");
                return BadRequest();
            }
        }



        [HttpPost("send-confirm-email-employer")]
        public IActionResult SendEmailEmployerSignUp([FromBody] EmailMessage<EmployerConfirmEmailBody> message)
        {
            try
            {
                var jobId = scheduledJobService.Enqueue(() => emailService.SendEmail(message));
                logger.LogInformation($"Sent email to {string.Join(", ", message.ToEmails)} with subject {message.Subject} - Job Id: {jobId}");
                return Ok(jobId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email");
                return BadRequest();
            }
        }


        [HttpPost("send-confirm-email-candidate")]
        public IActionResult SendEmailCandidateSignUp([FromBody] EmailMessage<CandidateConfirmEmailBody> message)
        {
            try
            {
                var jobId = scheduledJobService.Enqueue(() => emailService.SendEmail(message));
                logger.LogInformation($"Sent email to {string.Join(", ", message.ToEmails)} with subject {message.Subject} - Job Id: {jobId}");
                return Ok(jobId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email");
                return BadRequest();
            }
        }

        [HttpPost("send-forgot-password-email-employer")]
        public IActionResult SendEmailEmployerForgotPassword([FromBody] EmailMessage<ForgotPasswordEmailBody> message)
        {
            try
            {
                var jobId = scheduledJobService.Enqueue(() => emailService.SendEmail(message));
                logger.LogInformation($"Sent email to {string.Join(", ", message.ToEmails)} with subject {message.Subject} - Job Id: {jobId}");
                return Ok(jobId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email");
                return BadRequest();
            }
        }


        [HttpPost("send-forgot-password-email-candidate")]
        public IActionResult SendEmailCandidateForgotPassword([FromBody] EmailMessage<ForgotPasswordEmailBody> message)
        {
            try
            {
                var jobId = scheduledJobService.Enqueue(() => emailService.SendEmail(message));
                logger.LogInformation($"Sent email to {string.Join(", ", message.ToEmails)} with subject {message.Subject} - Job Id: {jobId}");
                return Ok(jobId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email");
                return BadRequest();
            }
        }

        [HttpPost("send-forgot-password-email-admin")]
        public IActionResult SendEmailAdminForgotPassword([FromBody] EmailMessage<ForgotPasswordEmailBody> message)
        {
            try
            {
                var jobId = scheduledJobService.Enqueue(() => emailService.SendEmail(message));
                logger.LogInformation($"Sent email to {string.Join(", ", message.ToEmails)} with subject {message.Subject} - Job Id: {jobId}");
                return Ok(jobId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email");
                return BadRequest();
            }
        }
    }
}
