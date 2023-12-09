using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IAdminAccountMetaRepository : IRepositoryBaseAsync<AdminAccountMeta,int,JobiContext>
    {
        
        /// Description: kiểm tra trong bảng employer meta có cặp key, value này không
        /// <param name="code"></param>
        /// <param name="hash"></param>
        /// <returns>kiểm tra trong bảng employer meta có cặp key, value này không</returns>
        Task<AdminAccountMeta> CheckVerifyCode(string code, string hash);

        
        /// Description: đếm số lần email đã gửi trong 1 h
        /// </summary>
        /// <param name="AdminAccountId"></param>
        /// <returns>Số lần email đã gửi</returns>
        Task<int> CountResetPassword(long AdminAccountId);

    }
}
