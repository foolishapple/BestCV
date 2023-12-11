using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage<EmployerConfirmEmailBody> message);
        void SendEmail(EmailMessage<EmployerConfirmEmailBody> message);
        void SendEmail(EmailMessage<CandidateConfirmEmailBody> message);
        void SendEmail(EmailMessage<ForgotPasswordEmailBody> message);
    }
}
