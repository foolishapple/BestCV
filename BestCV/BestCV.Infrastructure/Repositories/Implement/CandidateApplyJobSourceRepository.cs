using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CandidateApplyJobSourceRepository : RepositoryBaseAsync<CandidateApplyJobSource,int,JobiContext>, ICandidateApplyJobSourceRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public CandidateApplyJobSourceRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db,unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> IsNameExistAsync(string name , int id)
        {
            return await _db.CandidateApplyJobSources.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active );
        } 
    }
}
