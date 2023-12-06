using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IAdminAccountMetaRepository : IRepositoryBaseAsync<AdminAccountMeta,int,JobiContext>
    {
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 11/8/2023
        /// Description: kiểm tra trong bảng employer meta có cặp key, value này không
        /// <param name="code"></param>
        /// <param name="hash"></param>
        /// <returns>kiểm tra trong bảng employer meta có cặp key, value này không</returns>
        Task<AdminAccountMeta> CheckVerifyCode(string code, string hash);

        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 11/8/2023
        /// Description: đếm số lần email đã gửi trong 1 h
        /// </summary>
        /// <param name="AdminAccountId"></param>
        /// <returns>Số lần email đã gửi</returns>
        Task<int> CountResetPassword(long AdminAccountId);

    }
}
