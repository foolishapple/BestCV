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
    public interface IInterviewTypeRepository: IRepositoryBaseAsync<InterviewType, int, JobiContext>
    {
        /// <summary>
        /// Description: Check interview type name is existed
        /// </summary>
        /// <param name="name">interview type name</param>
        /// <param name="id">interview type id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
    }
}
