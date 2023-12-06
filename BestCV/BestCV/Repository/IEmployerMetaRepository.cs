using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerMetaRepository: IRepositoryBaseAsync<EmployerMeta, int, JobiContext >
    {
        /// <summary>
        /// author: truongthieuhuyen
        /// created: 29.07.2023
        /// </summary>
        /// <param name="code"></param>
        /// <param name="hash"></param>
        /// <returns>kiểm tra trong bảng employer meta có cặp key, value này không</returns>
        Task<EmployerMeta> CheckVerifyCode(string code, string hash);

        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: đếm số lần email đã gửi trong 1 h
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns>Số lần email đã gửi</returns>
        Task<int> CountResetPassword(long employerId);

    }
}
