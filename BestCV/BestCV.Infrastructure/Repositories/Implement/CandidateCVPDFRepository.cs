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
    public class CandidateCVPDFRepository : RepositoryBaseAsync<CandidateCVPDF, long, JobiContext>, ICandidateCVPDFRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public CandidateCVPDFRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }
    }
}
