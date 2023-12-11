using BestCV.Application.Models.EmployerWalletHistory;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerWalletHistoryService : IServiceQueryBase<long, InsertEmployerWalletHistoryDTO, UpdateEmployerWalletHistoryDTO, EmployerWalletHistoryDTO>
    {
        Task<DionResponse> ReportCVCandidate(ReportCandidateDTO model);
        Task<object> ListEmployerWalletHistories(DTParameters parameters);
        Task<DionResponse> QuickIsApprovedAsync(long id);
    }
}
