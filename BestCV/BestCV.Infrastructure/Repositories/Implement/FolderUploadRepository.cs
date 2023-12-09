using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
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
