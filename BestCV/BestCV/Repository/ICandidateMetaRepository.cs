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
    public interface ICandidateMetaRepository : IRepositoryBaseAsync<CandidateMeta, long, JobiContext>
    {
        Task<CandidateMeta> CheckVerifyCode(string code, string hash);

        Task<int> CountSendingEmail(long id);

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
