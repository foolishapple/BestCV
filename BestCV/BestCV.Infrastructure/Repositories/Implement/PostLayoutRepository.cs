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
    public class PostLayoutRepository : RepositoryBaseAsync<PostLayout, int, JobiContext>, IPostLayoutRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public PostLayoutRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            this.db = db;
            this.unitOfWork = unitOfWork;

        }
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">PostLayoutId</param>
        /// <param name="name">PostLayoutName</param>
        /// <returns>bool</returns>
        public async Task<bool> IsNameExistAsync(int id, string name)
        {
            return await db.PostLayouts.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);

        }
    }
}
