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
using System.Xml.Linq;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class PostCategoryRepository : RepositoryBaseAsync<PostCategory, int, JobiContext>, IPostCategoryRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public PostCategoryRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            this.db = db;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">PostCategoryId</param>
        /// <param name="color">PostCategoryColor</param>
        /// <returns>bool</returns>
        public async Task<bool> IsColorExistAsync(int id, string color)
        {
            return await db.PostCategories.AnyAsync(c => c.Color.ToLower().Trim() == color.ToLower().Trim() && c.Id != id && c.Active);

        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">PostCategoryId</param>
        /// <param name="name">PostCategoryName</param>
        /// <returns>bool</returns>
        public async Task<bool> IsNameExistAsync(int id, string name)
        {
            return await db.PostCategories.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);

        }
    }

}
