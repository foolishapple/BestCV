using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.UploadFiles;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IUploadFileRepository : IRepositoryBaseAsync<UploadFile,int,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 31/07/2023
        /// Description: get list paging upload file
        /// </summary>
        /// <param name="parameters">paging upload file parameters</param>
        /// <returns></returns>
        Task<PagingData<List<UploadFile>>> ListPaging(PagingUploadFileParameter parameters);
    }
}
