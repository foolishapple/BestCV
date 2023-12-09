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
    public class SlideRepository : RepositoryBaseAsync<Slide, int, JobiContext>, ISlideRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public SlideRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check slide name is existed
        /// </summary>
        /// <param name="name">slide name</param>
        /// <param name="id">slide id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            return await _db.Slides.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
        public async Task<bool> CandidateOrderSortAsync(int ordersort , int id)
        {
            return await _db.Slides.AnyAsync(c => c.CandidateOrderSort == ordersort && c.Active && c.Id != id);
        }
       public async Task<List<Slide>> GetAllSort()
        {
            return await _db.Slides.Where(s => s.Active).OrderBy(s => s.CandidateOrderSort).ThenBy(s => s.SubOrderSort).ToListAsync();
        }
        public async Task<int> MaxSubSort(int ordersort)
        {
            var maxSubSort = await _db.Slides
                .Where(s => s.CandidateOrderSort == ordersort && s.Active)
                .Select(s => (int?)s.SubOrderSort) 
                .MaxAsync() ?? -1; 

            return maxSubSort;
        }
        public async Task<bool> ChangeOrderSort(List<Slide> objs)
        {
            foreach (var obj in objs)
            {
                _db.Attach(obj);
                _db.Entry(obj).Property(c => c.CandidateOrderSort).IsModified = true;
                _db.Entry(obj).Property(c => c.SubOrderSort).IsModified = true;
            }
            return await _db.SaveChangesAsync() == objs.Count();
        }

        public async Task<List<Slide>> ListSlideShowonHomepage()
        {
            return await _db.Slides.Where(x => x.Active)
                .OrderBy(x => x.CandidateOrderSort)
                .ThenBy(x => x.SubOrderSort).Take(5)
                .ToListAsync();
        }

    }
}
