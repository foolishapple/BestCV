using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates;
using BestCV.Domain.Aggregates.JobType;
using BestCV.Domain.Aggregates.Post;
using BestCV.Domain.Aggregates.PostCategory;
using BestCV.Domain.Aggregates.PostType;
using BestCV.Domain.Aggregates.Tag;
using BestCV.Domain.Constants;
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
    public class PostRepository : RepositoryBaseAsync<Post, int, JobiContext>, IPostRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public PostRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            this.db = db;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 02/08/2023
        /// Description: approve post
        /// </summary>
        /// <param name="obj">Post</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateApproveStatusPostAsync(Post obj)
        {
            db.Attach(obj);
            db.Entry(obj).Property(c => c.IsApproved).IsModified = true;
            db.Entry(obj).Property(c => c.ApprovalDate).IsModified = true;
            return await db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: Detail PostAggregates
        /// </summary>
        /// <param name="id">PostId</param>
        /// <returns>PostAggregates</returns>
        public async Task<PostAggregates> DetailPostByIdAsync(int id)
        {
            return await (from p in db.Posts
                          join pc in db.PostCategories on p.PostCategoryId equals pc.Id
                          join pt in db.PostTypes on p.PostTypeId equals pt.Id
                          join ps in db.PostStatuses on p.PostStatusId equals ps.Id
                          join a in db.AdminAccounts on p.AuthorId equals a.Id
                          join pl in db.PostLayouts on p.PostLayoutId equals pl.Id
                          where p.Id == id && p.Active == true && pc.Active == true && pt.Active == true && a.Active && pl.Active && ps.Active
                          let tags = (from ptag in db.PostTags
                                      join tag in db.Tags on ptag.TagId equals tag.Id
                                      where tag.Active && ptag.PostId == p.Id
                                      select new TagAggregates
                                      {
                                          Id = tag.Id,
                                          Active = tag.Active,
                                          CreatedTime = tag.CreatedTime,
                                          Name = tag.Name,
                                          TagTypeId = tag.TagTypeId,
                                      }).ToList()
                          select new PostAggregates
                          {
                              Id = p.Id,
                              Active = p.Active,
                              Search = p.Search,
                              AuthorId = p.AuthorId,
                              CreatedTime = p.CreatedTime,
                              IsApproved = p.IsApproved,
                              Name = p.Name,
                              Photo = p.Photo,
                              PostCategoryId = p.PostCategoryId,
                              PostCategoryName = pc.Name,
                              PostLayoutId = p.PostLayoutId,
                              PostLayoutName = pl.Name,
                              PostTypeId = p.PostTypeId,
                              PostTypeName = pt.Name,
                              Description = p.Description,
                              IsDraft = p.IsDraft,
                              IsPublish = p.IsPublish,
                              Overview = p.Overview,
                              PostStatusId = p.PostStatusId,
                              PostStatusName = ps.Name,
                              ApprovalDate = p.ApprovalDate,
                              AuthorName = a.FullName,
                              PublishedTime = p.PublishedTime,
                              ListTag = tags,
                          }).FirstOrDefaultAsync();

        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 28/07/2023
        /// Description: check name is exist in same category
        /// </summary>
        /// <param name="id">PostId</param>
        /// <param name="postCategoryId">postCategoryId</param>
        /// <param name="name">PostName</param>
        /// <returns>bool</returns>
        public async Task<bool> IsNameExistInSameCategoryAsync(int id, int postCategoryId, string name)
        {
            return await db.Posts.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.PostCategoryId == postCategoryId && c.Id != id && c.Active);

        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: list PostDTO
        /// </summary>
        /// <param name="parameters">DataTableModel DTParamenters</param>
        /// <returns>object</returns>
        public async Task<object> ListPostAggregatesAsync(DTParameters parameters)
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

            
            var result = await (
                from p in db.Posts
                join pt in db.PostTypes on p.PostTypeId equals pt.Id
                join pc in db.PostCategories on p.PostCategoryId equals pc.Id
                join ps in db.PostStatuses on p.PostStatusId equals ps.Id
                join pl in db.PostLayouts on p.PostLayoutId equals pl.Id
                join aa in db.AdminAccounts on p.AuthorId equals aa.Id

                where p.Active && pt.Active && pc.Active && pl.Active && aa.Active
                select new PostAggregates
                {
                    Id = p.Id,
                    Active = p.Active,
                    AuthorId = p.AuthorId,
                    AuthorName = aa.FullName,
                    CreatedTime = p.CreatedTime,
                    Description = p.Description,
                    IsApproved = p.IsApproved,
                    IsDraft = p.IsDraft,
                    IsPublish = p.IsPublish,
                    Name = p.Name,
                    Overview = p.Overview,
                    Photo = p.Photo,
                    PostCategoryId = p.PostCategoryId,
                    PostCategoryName = pc.Name,
                    PostLayoutId = p.PostLayoutId,
                    PostLayoutName = pl.Name,
                    PostStatusId = p.PostStatusId,
                    PostStatusName = ps.Name,
                    PostTypeId = p.PostTypeId,
                    PostTypeName = pt.Name,
                    Search = p.Search,
                    PostStatusColor = ps.Color,


                }
                ).ToListAsync();

            var recordsTotal = result.Count;

            result = result.Where(s => string.IsNullOrEmpty(keyword) || s.Name.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.PostTypeName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.PostCategoryName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.Name.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.AuthorName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.PostCategoryName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.PostStatusName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.CreatedTime.ToCustomString().Contains(keyword)

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
                        result = result.Where(r => r.Name.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;

                    case "authorName":
                        result = result.Where(r => r.AuthorName.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "postTypeName":
                        var postTypeIdsArr = search.Split("|").Select(n => Int32.Parse(n));
                        result = result.Where(r => postTypeIdsArr.Contains(r.PostTypeId)).ToList();
                        break;
                    case "postCategoryName":
                        var postCategoryIdsArr = search.Split("|").Select(n => Int32.Parse(n));
                        result = result.Where(r => postCategoryIdsArr.Contains(r.PostCategoryId)).ToList();
                        break;
                    case "postStatusName":
                        if (search != "null")
                        {
                            var searchStatusId = Int32.Parse(search);
                            result = result.Where(r => searchStatusId == r.PostStatusId).ToList();
                        }

                        break;

                    case "isApproved":
                        if (search != "null")
                        {
                            result = result.Where(r => search.Contains(r.IsApproved ? "true" : "false")).ToList();
                        }

                        break;
                    case "createdTime":
                        var searchDateArrs = search.Split(',');

                        if (searchDateArrs.Length == 2)
                        {
                            //Không có ngày bắt đầu
                            if (string.IsNullOrEmpty(searchDateArrs[0]))
                            {
                                result = result.Where(r => r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                            //không có ngày kết thúc
                            else if (string.IsNullOrEmpty(searchDateArrs[1]))
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0])).ToList();
                            }
                            //có cả 2
                            else
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0]) && r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                        }

                        break;
                }
            }
            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();


            var data = new
            {
                draw = parameters.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = result.Count,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList()
            };
            return data;
        }

        public async Task<List<PostAggregates>> ListPostShowonHomepage()
        {
            return await (from row in db.Posts
                          from pt in db.PostTypes
                          from ps in db.PostStatuses
                          from pc in db.PostCategories
                          from acc in db.AdminAccounts
                          where row.PostTypeId == pt.Id && row.PostCategoryId == pc.Id && ps.Id == row.PostStatusId
                          && row.Active && pt.Active && ps.Active && pc.Active && row.AuthorId == acc.Id
                          orderby row.CreatedTime
                          select new PostAggregates
                          {
                              Active = row.Active,
                              PostLayoutId = row.PostLayoutId,
                              PostStatusId = row.PostStatusId,
                              Id = row.Id,
                              PostCategoryId = row.PostCategoryId,
                              PostTypeId = row.PostTypeId,
                              ApprovalDate = row.ApprovalDate,
                              AuthorId = row.AuthorId,
                              AuthorName = acc.FullName,
                              CreatedTime = row.CreatedTime,
                              Description = row.Description,
                              IsApproved = row.IsApproved,
                              IsDraft = row.IsDraft,
                              IsPublish = row.IsPublish,
                              Name = row.Name,
                              Overview = row.Overview,
                              Photo = row.Photo,
                              PostCategoryName = pc.Name,
                              PostStatusColor = ps.Color,
                              PostStatusName = ps.Name,
                              PostTypeName = pt.Name,
                              PublishedTime = row.PublishedTime,
                              Search = row.Search,
                          }).Take(4).ToListAsync();
        }

        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 21/08/2023
        /// </summary>
        /// <returns></returns>
        public async Task<object> LoadDataFilterPostHomePageAsync()
        {
            //Lấy data loại bài viết
            var postCategoryData = await (from pc in db.PostCategories
                                          from p in db.Posts
                                          where pc.Active && p.Active && p.IsApproved
                                          select new FilterByPostCategoryAggregates
                                          {
                                              Id = pc.Id,
                                              Name = pc.Name,
                                              CountPost = (from row in db.Posts
                                                           from row1 in db.PostCategories
                                                           where row1.Id == row.PostCategoryId && row.Active && row1.Active && row.IsApproved && row.PostCategoryId == pc.Id
                                                           select row).Count()
                                          }).Distinct().ToListAsync();
            //Lấy data thẻ bài viết
            var postTagsData = await (from tag in db.Tags
                                      from p in db.Posts
                                      where tag.Active && p.Active && p.IsApproved
                                      select new TagAggregates
                                      {
                                          Id = tag.Id,
                                          TagTypeId = tag.Id
                                      }).Distinct().ToListAsync();

            return new
            {
                PostCategoryData = postCategoryData,
                PostTagsData = postCategoryData
            };
        }

        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 21/08/2023
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<PagingData<List<PostAggregates>>> ListPostHomePageAsync(PostParameters parameter)
        {
            //Khai báo biến
            var keyword = parameter.Keywords;
            var pageIndex = parameter.PageIndex;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            var pageSize = parameter.PageSize;
            var ListTag = parameter.ListTag;
            var tagId = parameter.TagId;

            if (parameter.OrderCriteria != null)
            {
                orderCriteria = parameter.OrderCriteria;
                orderAscendingDirection = parameter.OrderCriteria.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            //Lấy dữ liệu
            var result = await (
                from row in db.Posts
                from pt in db.PostTypes
                from pc in db.PostCategories
                from ps in db.PostStatuses
                from pl in db.PostLayouts
                from aa in db.AdminAccounts

                where row.Active && pt.Active && pc.Active && pl.Active && aa.Active && ps.Active 
                && row.PostCategoryId == pc.Id
                && row.PostStatusId == ps.Id
                && row.PostTypeId == pt.Id
                && row.PostLayoutId == pl.Id
                && row.AuthorId == aa.Id

                && row.IsApproved
                select new PostAggregates
                {
                    Id = row.Id,
                    Active = row.Active,
                    AuthorId = row.AuthorId,
                    AuthorName = aa.FullName,
                    CreatedTime = row.CreatedTime,
                    Description = row.Description,
                    IsApproved = row.IsApproved,
                    IsDraft = row.IsDraft,
                    IsPublish = row.IsPublish,
                    Name = row.Name,
                    Overview = row.Overview,
                    Photo = row.Photo,
                    PostCategoryId = row.PostCategoryId,
                    PostCategoryName = pc.Name,
                    PostLayoutId = row.PostLayoutId,
                    PostLayoutName = pl.Name,
                    PostStatusId = row.PostStatusId,
                    PostStatusName = ps.Name,
                    PostTypeId = row.PostTypeId,
                    PostTypeName = pt.Name,
                    Search = row.Search,
                    PostStatusColor = ps.Color,
                    PublishedTime = row.PublishedTime,
                    ListTag = (from tags in db.Tags
                               from postTag in db.PostTags
                               where tags.Active && postTag.Active && postTag.PostId == row.Id && postTag.TagId == tags.Id
                               select new TagAggregates
                               {
                                   Id = tags.Id,
                                   Name = tags.Name,
                               }).ToList(),
                }).ToListAsync();

            var allRecord = result.Count();

            //Tìm kiếm tất cả 
            result = result.Where(s => string.IsNullOrEmpty(keyword) || s.Name.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.PostTypeName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.AuthorName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            s.PostCategoryName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) ||
            (!string.IsNullOrEmpty(s.Search) && s.Search.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
                ).ToList();

            //Filter theo loại bài viết
            if (parameter.PostCategory.Length > 0)
            {
                result = result.Where(x => parameter.PostCategory.Contains(x.PostCategoryId.ToString())).ToList();
            }

            //Filter theo thẻ bài viết 
            if (parameter.ListTag.Length > 0)
            {
                var arrPostTag = parameter.ListTag.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListTag.Any(c => arrPostTag.Contains(c.Id))).ToList();
            }

            //Orderby
            if (parameter.OrderCriteria != null)
            {
                switch (parameter.OrderCriteria)
                {
                    case "asc":
                        result = result.OrderBy(x => x.Id).ToList();
                        break;
                    case "desc":
                        result = result.OrderByDescending(x => x.CreatedTime).ToList();
                        break;

                }
            }

            var recordsTotal = result.Count;
            return new PagingData<List<PostAggregates>>
            {
                DataSource = result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }
    }
}
