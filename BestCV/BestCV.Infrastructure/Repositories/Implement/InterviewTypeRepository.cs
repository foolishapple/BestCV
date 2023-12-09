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
    public class InterviewTypeRepository : RepositoryBaseAsync<InterviewType, int, JobiContext>, IInterviewTypeRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public InterviewTypeRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check interview type name is existed
        /// </summary>
        /// <param name="name">interview type name</param>
        /// <param name="id">interview type id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            return await _db.InterviewTypes.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
    }
}
