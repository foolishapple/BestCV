using BestCV.Application.Models.Company;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Company;
using BestCV.Domain.Aggregates.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICompanyService : IServiceQueryBase<int, InsertCompanyDTO, UpdateCompanyDTO, CompanyDTO>
    {
        public Task<bool> IsNameExist(string name);

        public Task<BestCVResponse> GetDetailByEmnployerId(long employerId);

        public Task<object> ListCompanyAggregate(DTParameters parameters);
        Task<BestCVResponse> DetailAdmin(int id);
        Task<BestCVResponse> ExportExcel(List<CompanyAggregates> data);

        Task<BestCVResponse> GetCompanyAggregatesById(int companyId);
        byte[] DownloadExcel(string fileGuid, string fileName);

 
        public Task<BestCVResponse> SearchCompanyHomePageAsync(SearchingCompanyParameters parameter);
    }
}
