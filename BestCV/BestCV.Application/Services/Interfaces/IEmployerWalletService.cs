using BestCV.Application.Models.EmployerWallet;
using BestCV.Application.Models.ExperienceRange;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerWalletService : IServiceQueryBase<long, InsertEmployerWalletDTO, UpdateEmployerWalletDTO, EmployerWalletDTO>
    {
        Task<DionResponse> GetCreditWalletByEmployerId(long employerId);
    }
}
