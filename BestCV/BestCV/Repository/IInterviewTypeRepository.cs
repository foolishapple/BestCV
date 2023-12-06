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
    public interface IInterviewTypeRepository: IRepositoryBaseAsync<InterviewType, int, JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Check interview type name is existed
        /// </summary>
        /// <param name="name">interview type name</param>
        /// <param name="id">interview type id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
    }
}
