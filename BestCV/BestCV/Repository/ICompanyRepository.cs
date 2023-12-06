﻿using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.Company;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICompanyRepository : IRepositoryBaseAsync<Company, int, JobiContext>
    {
        public Task<bool> IsNameExist(string name);
        public Task<Company> GetDetailByEmnployerId(long employerId);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 09/08/2023
        /// Description : Lấy chi tiết tổ chức tuyển dụng
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<CompanyAggregates> GetCompanyAggregatesById(int companyId);

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 07/08/2023
        /// Description : Lấy danh sách tổ chức tuyển dụng (ServerSide)
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns></returns>
        Task<object> ListCompanyAggregates(DTParameters parameters);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 17/08/2023
        /// Description : Tìm kiếm công ty trang chủ (server side)
        /// </summary>
        /// <param name="param">SearchingCompanyParameters</param>
        /// <returns></returns>
        public Task<PagingData<List<CompanyAggregates>>> SearchCompanyHomePageAsync(SearchingCompanyParameters parameter);
    }
}
