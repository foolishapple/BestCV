using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using HandlebarsDotNet;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace BestCV.Application.Services.Implement
{
    public class EmailService : IEmailService
    {
        private SmtpSettings _smtpSettings;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public EmailService( ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<EmailService>();
            _configuration = configuration;
        }
        public async Task SendEmailAsync(EmailMessage<EmployerConfirmEmailBody> message)
        {
            _smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
            var smtpClient = CreateSmtpClient();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.UserName, _smtpSettings.SenderName),
                Subject = message.Subject,
                Body = await GetEmailTemplateAsync(message.TemplatePath, message.Model),
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8,
            };

            foreach (var toEmail in message.ToEmails)
            {
                mailMessage.To.Add(toEmail);
            }

            if (message.CcEmails != null)
            {
                foreach (var ccEmail in message.CcEmails)
                {
                    mailMessage.CC.Add(ccEmail);
                }
            }

            if (message.BccEmails != null)
            {
                foreach (var bccEmail in message.BccEmails)
                {
                    mailMessage.Bcc.Add(bccEmail);
                }
            }

            await smtpClient.SendMailAsync(mailMessage);
        }

        private async Task<string> GetEmailTemplateAsync(string templatePath, object model)
        {
            var templateContent = await File.ReadAllTextAsync(templatePath);
            var template = Handlebars.Compile(templateContent);
            var body = template(model);

            return body;
        }

        private string GetEmailTemplate(string templatePath, object model)
        {
            var templateContent = File.ReadAllText(templatePath);
            var template = Handlebars.Compile(templateContent);
            var body = template(model);

            return body;
        }

        private SmtpClient CreateSmtpClient()
        {
            _smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
            var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password),
                TargetName = _smtpSettings.TargetName,
                EnableSsl = _smtpSettings.UseSsl
            };

            return smtpClient;
        }

        public void SendEmail(EmailMessage<EmployerConfirmEmailBody> message)
        {
            _smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
            var smtpClient = CreateSmtpClient();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.UserName, _smtpSettings.SenderName),
                Subject = message.Subject,
                Body = GetEmailTemplate(message.TemplatePath, message.Model),
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8,
            };
            _logger.LogInformation(mailMessage.Body);
            foreach (var toEmail in message.ToEmails)
            {
                mailMessage.To.Add(toEmail);
            }

            if (message.CcEmails != null)
            {
                foreach (var ccEmail in message.CcEmails)
                {
                    mailMessage.CC.Add(ccEmail);
                }
            }

            if (message.BccEmails != null)
            {
                foreach (var bccEmail in message.BccEmails)
                {
                    mailMessage.Bcc.Add(bccEmail);
                }
            }

            smtpClient.Send(mailMessage);
        }

        public void SendEmail(EmailMessage<CandidateConfirmEmailBody> message)
        {
            _smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
            var smtpClient = CreateSmtpClient();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.UserName, _smtpSettings.SenderName),
                Subject = message.Subject,
                Body = GetEmailTemplate(message.TemplatePath, message.Model),
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8,
            };
            _logger.LogInformation(mailMessage.Body);
            foreach (var toEmail in message.ToEmails)
            {
                mailMessage.To.Add(toEmail);
            }

            if (message.CcEmails != null)
            {
                foreach (var ccEmail in message.CcEmails)
                {
                    mailMessage.CC.Add(ccEmail);
                }
            }

            if (message.BccEmails != null)
            {
                foreach (var bccEmail in message.BccEmails)
                {
                    mailMessage.Bcc.Add(bccEmail);
                }
            }

            smtpClient.Send(mailMessage);
        }
        public void SendEmail(EmailMessage<ForgotPasswordEmailBody> message)
        {
            var smtpClient = CreateSmtpClient();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.UserName, _smtpSettings.SenderName),
                Subject = message.Subject,
                Body = GetEmailTemplate(message.TemplatePath, message.Model),
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8,
            };
            _logger.LogInformation(mailMessage.Body);
            foreach (var toEmail in message.ToEmails)
            {
                mailMessage.To.Add(toEmail);
            }

            if (message.CcEmails != null)
            {
                foreach (var ccEmail in message.CcEmails)
                {
                    mailMessage.CC.Add(ccEmail);
                }
            }

            if (message.BccEmails != null)
            {
                foreach (var bccEmail in message.BccEmails)
                {
                    mailMessage.Bcc.Add(bccEmail);
                }
            }

            smtpClient.Send(mailMessage);
        }
    }
}
