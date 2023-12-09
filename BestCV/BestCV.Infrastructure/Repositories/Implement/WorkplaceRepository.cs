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
    public class WorkplaceRepository : RepositoryBaseAsync<WorkPlace, int, JobiContext>, IWorkplaceRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public WorkplaceRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 27/07/2023
        /// Description: Lấy dánh sách quận huyện theo ID tỉnh thành
        /// </summary>
        /// <param name="cityId">ID tỉnh thành</param>
        /// <returns>Danh sách quận huyện</returns>
        public async Task<List<WorkPlace>> GetListDistrictByCityIdAsync(int cityId)
        {
            return await db.WorkPlaces.Where(e => e.ParentId == cityId && e.Active).ToListAsync();
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 27/07/2023
        /// Description: Lấy danh sách tất cả tỉnh thành
        /// </summary>
        /// <returns></returns>
        public async Task<List<WorkPlace>> GetListCityAsync()
        {
            return await db.WorkPlaces.Where(e => e.ParentId == null && e.Active).ToListAsync();
        }
    }
}
