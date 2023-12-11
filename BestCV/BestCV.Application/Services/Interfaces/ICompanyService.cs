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

        public Task<DionResponse> GetDetailByEmnployerId(long employerId);
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 07/08/2023
        /// Description : Lấy danh sách tổ chức tuyển dụng (Server Side)
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns></returns>
        public Task<object> ListCompanyAggregate(DTParameters parameters);
        Task<DionResponse> DetailAdmin(int id);
        Task<DionResponse> ExportExcel(List<CompanyAggregates> data);
        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 09/08/2023
        /// Description : Lấy chi tiết tổ chức tuyển dụng
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<DionResponse> GetCompanyAggregatesById(int companyId);
        byte[] DownloadExcel(string fileGuid, string fileName);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 17/08/2023
        /// Description : Tìm kiếm công ty trang chủ (server side)
        /// </summary>
        /// <param name="parameter">SearchingCompanyParameters</param>
        /// <returns></returns>
        public Task<DionResponse> SearchCompanyHomePageAsync(SearchingCompanyParameters parameter);
    }
}
