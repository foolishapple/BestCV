using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.CompanySize;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class CompanySizeRepository : RepositoryBaseAsync<CompanySize, int, JobiContext>, ICompanySizeRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public CompanySizeRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 07/08/2023
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsCompanySizeExistAsync(string name, int id)
        {
            return await db.CompanySizes.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 17/08/2023
        /// Description : lấy ra thông tin của quy mô công ty và số lượng công ty thuộc quy mô này
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<object> LoadDataFilterCompanySizeHomePageAsync()
        {
            //lấy data quy mô công ty
            //var CompanySizeData = await (from cs in db.CompanySizes
            //                             join c in db.Companies on cs.Id equals c.CompanySizeId
            //                             join e in db.Employers on c.EmployerId equals e.Id
            //                             where cs.Active && c.Active && e.Active
            //                             group cs by new { cs.Id, cs.Name } into grouped
            //                             select new CompanySizeAggregates
            //                             {
            //                                 Id = grouped.Key.Id,
            //                                 Name = grouped.Key.Name,
            //                                 Count = grouped.Count()
            //                             }).ToListAsync();
            var CompanySizeData = await (from cs in db.CompanySizes
                                       from c in db.Companies
                                       where cs.Active && c.Active
                                       select new CompanySizeAggregates
                                       {
                                           Id = cs.Id,
                                           Name = cs.Name,
                                           Count = (from row in db.CompanySizes
                                                       from row1 in db.Companies
                                                       from e in db.Employers
                                                       where row.Id == row1.CompanySizeId && 
                                                             row.Active && row1.Active && 
                                                             row.Id == cs.Id && row1.EmployerId == e.Id && e.Active
                                                       select row).Count()
                                       }).Distinct().ToListAsync();
            return CompanySizeData;
        }
    }
}
