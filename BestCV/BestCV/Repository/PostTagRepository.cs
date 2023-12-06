using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class PostTagRepository : RepositoryBaseAsync<PostTag, int, JobiContext>, IPostTagRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public PostTagRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            this.db = db;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<PostTag>> ListByPostId(int postId)
        {
            return await db.PostTags.Where(s => s.Active && s.PostId == postId).ToListAsync();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 02/08/2023
        /// Description: list tag id by post id
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>List<int></returns>
        public async Task<List<int>> ListTagIdByPostId(int postId)
        {
            return await db.PostTags.Where(s => s.Active && s.PostId == postId).Select(s => s.TagId).ToListAsync();
        }
    }

}
