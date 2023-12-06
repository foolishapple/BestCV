using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.CandidateNotification;
using Jobi.Domain.Aggregates.Tag;
using Jobi.Domain.Constants;
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
    public class TagRepository : RepositoryBaseAsync<Tag, int, JobiContext>, ITagRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public TagRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 18.08.2023
        /// Description: check name is exist for type job
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> IsNameTypeJobExistAsync(int id, string name)
        {
            return await db.Tags.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active && c.TagTypeId == TagTypeId.JOB);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">TagId</param>
        /// <param name="name">TagName</param>
        /// <returns>bool</returns>
        public async Task<bool> IsNameTypePostExistAsync(int id, string name)
        {
            return await db.Tags.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active && c.TagTypeId == TagTypeId.POST);

        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: list tag
        /// </summary>
        /// <param name="obj">TagForSelect2Aggregates</param>
        /// <returns>object</returns>
        public async Task<object> ListSelectTagAsync(TagForSelect2Aggregates obj)
        {
            var query = db.Tags.Where(c => c.Active && c.TagTypeId == obj.TagTypeId).Select(c => new 
            {
                Text = c.Name,
                Id = c.Id
            });
            //2. Filter
            if (!String.IsNullOrEmpty(obj.Keyword))
            {
                query = query.Where(c => c.Text.ToLower().Trim().Contains(obj.Keyword.ToLower().Trim()));
            }
            var totalRecord = await query.CountAsync();
            //3. Return data
            return new 
            {
                ListData = await query.Skip(obj.PageIndex).Take(obj.PageSize).ToListAsync(),
                TotalRecord = totalRecord
            };
        }

        public async Task<List<Tag>> ListTagTypeJob()
        {
            var data = await db.Tags.Where(t => t.Active && t.TagTypeId == TagTypeId.JOB).ToListAsync();
            return data;
        }

        public async Task<object> ListTagAggregatesAsync(DTParameters parameters)
        {
            var keyword = parameters.Search?.Value;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (parameters.Order != null)
            {
                orderCriteria = parameters.Columns[parameters.Order[0].Column].Data;
                orderAscendingDirection = parameters.Order[0].Dir.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var query = await (
                from c in db.Tags
                join nt in db.TagTypes on c.TagTypeId equals nt.Id
                
                where c.Active && nt.Active 
                select new TagAggregates
                {
                    Active = c.Active,
                    CreatedTime = c.CreatedTime,
                    TagTypeId = c.TagTypeId,
                    TagTypeName = nt.Name,
                    Name = c.Name,
                    Id = c.Id,

                }).ToListAsync();
            var recordsTotal = query.Count;
            query = query.Where(s => string.IsNullOrEmpty(keyword) || s.Name.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.TagTypeName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            ).ToList();

            foreach (var column in parameters.Columns)
            {
                var search = column.Search.Value;
                if (!search.Contains("/"))
                {
                    search = column.Search.Value.ToLower().RemoveVietnamese();
                }
                if (string.IsNullOrEmpty(search)) continue;
                switch (column.Data)
                {
                    case "name":
                        query = query.Where(r => r.Name.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "tagTypeName":
                        var tagTypeArr = search.Split(",").Select(n => Int32.Parse(n));
                        query = query.Where(r => tagTypeArr.Contains(r.TagTypeId)).ToList();
                        break;
                    case "createdTime":
                        var searchDateArrs = search.Split(',');
                        if (searchDateArrs.Length == 2)
                        {
                            //Không có ngày bắt đầu
                            if (string.IsNullOrEmpty(searchDateArrs[0]))
                            {
                                query = query.Where(r => r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                            //không có ngày kết thúc
                            else if (string.IsNullOrEmpty(searchDateArrs[1]))
                            {
                                query = query.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0])).ToList();
                            }
                            //có cả 2
                            else
                            {
                                query = query.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0]) && r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                        }

                        break;
                }
            }
            query = orderAscendingDirection ? query.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : query.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();

            var data = new
            {
                draw = parameters.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = query.Count,

                data = query
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList()
            };
            return data;
        }

        public async Task<bool> IsNameExisAsync(string name, int id)
        {
            return await db.Tags.AnyAsync(c => c.Active && c.Name.ToLower() == name.ToLower().Trim() && c.Id != id);
        }
    }
}
