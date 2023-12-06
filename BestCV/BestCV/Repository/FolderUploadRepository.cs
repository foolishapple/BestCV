using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class FolderUploadRepository : RepositoryBaseAsync<FolderUpload,int,JobiContext>,IFolderUploadRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;

        public FolderUploadRepository(IUnitOfWork<JobiContext> unitOfWork, JobiContext db): base(db,unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
    }
}
