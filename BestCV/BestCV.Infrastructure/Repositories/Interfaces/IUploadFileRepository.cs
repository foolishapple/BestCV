using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.UploadFiles;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IUploadFileRepository : IRepositoryBaseAsync<UploadFile,int,JobiContext>
    {
        /// <summary>
        /// Description: get list paging upload file
        /// </summary>
        /// <param name="parameters">paging upload file parameters</param>
        /// <returns></returns>
        Task<PagingData<List<UploadFile>>> ListPaging(PagingUploadFileParameter parameters);
    }
}
