using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class CandidateApplyJobStatusRepository : RepositoryBaseAsync<CandidateApplyJobStatus, int, JobiContext>, ICandidateApplyJobStatusRepository
    {
        private readonly JobiContext _db;
        private IUnitOfWork<JobiContext> _unitOfWork;
        public CandidateApplyJobStatusRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db,unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: Check candidate appply job status name is existed
        /// </summary>
        /// <param name="color">candidate apply job status color</param>
        /// <param name="id">candidate apply job id</param>
        /// <returns></returns>
        public async Task<bool> ColorIsExisted(string color, int id)
        {
            return await _db.CandidateApplyJobStatuses.AnyAsync(c => c.Active && c.Color == color && c.Id != id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: Check candidate appply job status name is existed
        /// </summary>
        /// <param name="name">candidate apply job status name</param>
        /// <param name="id">candidate apply job status id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            string lowerName = name.ToLower();
            return await _db.CandidateApplyJobStatuses.AnyAsync(c => c.Active && c.Name.ToLower() == lowerName.ToLower() && c.Id != id);
        }
    }
}
