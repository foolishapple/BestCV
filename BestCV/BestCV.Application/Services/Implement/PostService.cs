using AutoMapper;
using HandlebarsDotNet.Features;
using BestCV.Application.Models.Post;
using BestCV.Application.Models.PostLayout;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestCV.Domain.Constants;
using System.Collections;
using BestCV.Application.Models.PostCategory;
using BestCV.Application.Models.JobSkill;
using BestCV.Application.Models.Tag;

namespace BestCV.Application.Services.Implement
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IPostTagRepository postTagRepository;
        private readonly ILogger<IPostService> logger;
        private readonly IMapper mapper;
        private readonly IPostCategoryRepository postCategoryRepository;
        private readonly ITagRepository tagRepository;

        public PostService(IPostRepository postRepository, IPostTagRepository postTagRepository, ILoggerFactory loggerFactory, IMapper mapper, IPostCategoryRepository postCategoryRepository, ITagRepository tagRepository)
        {
            this.mapper = mapper;
            this.postRepository = postRepository;
            this.postTagRepository = postTagRepository;
            this.logger = loggerFactory.CreateLogger<IPostService>();
            this.postCategoryRepository = postCategoryRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<DionResponse> UpdateApproveStatusPostAsync(ApprovePostDTO obj)
        {
            var data = await postRepository.GetByIdAsync(obj.Id);
            if (data==null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }

            var updateObj = mapper.Map(obj, data);

            if (updateObj.IsApproved)
            {
                updateObj.ApprovalDate = DateTime.Now;
            }

            if (await postRepository.UpdateApproveStatusPostAsync(updateObj))
            {

                return DionResponse.Success("Duyệt bài viết thành công.");
            }
            else
            {
                return DionResponse.Error("Duyệt bài viết không thành công.");

            }

        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 28/07/2023
        /// Description: create post 
        /// </summary>
        /// <param name="obj">InsertPostDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertPostDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await postRepository.IsNameExistInSameCategoryAsync(0, obj.PostCategoryId, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var newObj = mapper.Map<Post>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;
            newObj.Overview = newObj.Overview.ToEscape();
            newObj.Name = newObj.Name.ToEscape();
			newObj.Search = newObj.Name.ToLower().RemoveVietnamese() + "_" + (newObj.IsApproved ? "duyet" : "chua duyet") + "_" + newObj.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss");
			newObj.AuthorId = 1001;
            if (newObj.IsPublish)
            {
                newObj.PublishedTime = DateTime.Now;
            }

            if (newObj.IsApproved)
            {
                newObj.ApprovalDate = DateTime.Now;
            }
            var database = await postRepository.BeginTransactionAsync();
			using (database)
			{
				try
				{

                    await postRepository.CreateAsync(newObj);
                    await postRepository.SaveChangesAsync();

                    if (newObj.Id>0)
					{
                        if (!String.IsNullOrEmpty(obj.TagIds))
                        {
							var listPostTag = new List<PostTag>();

                            var tagId = obj.TagIds.Split(" ");
                            foreach (var i in tagId)
                            {
                                listPostTag.Add(new PostTag()
                                {
                                    TagId = int.Parse(i),
									Id=0,
									Active = true,
									CreatedTime = DateTime.Now,
									PostId = newObj.Id,
                                    
                                });
                            }

							await postTagRepository.CreateListAsync(listPostTag);
                            await postTagRepository.SaveChangesAsync();
                        }

                        await postRepository.EndTransactionAsync();
                        return DionResponse.Success("Thêm mới bài viết thành công.");
                    }
                    else
                    {
                        await postRepository.RollbackTransactionAsync();
                        return DionResponse.Error("Thêm mới bài viết không thành công.");
                    }
                }
				catch (Exception e)
				{

                    logger.LogError(e, $"Failed to insert post: {obj}");
					await postRepository.RollbackTransactionAsync();
					return DionResponse.Error("Thêm mới bài viết không thành công.");
                }
            }

		}

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertPostDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
			var data = await postRepository.DetailPostByIdAsync(id);
			if (data == null)
			{
				return DionResponse.NotFound("Không có dữ liệu.", id);
			}
			return DionResponse.Success(data);
		}


        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 28/07/2023
        /// Description: get list PostAggregates
        /// </summary>
        /// <param name="parameters">DataTableModel DTParamenters</param>
        /// <returns>DionResponse</returns>
        public async Task<object> ListPostAggregatesAsync(DTParameters parameters)
        {
            return await postRepository.ListPostAggregatesAsync(parameters);
        }

        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
			var data = await postRepository.SoftDeleteAsync(id);
			if (data)
			{
				await postRepository.SaveChangesAsync();
				return DionResponse.Success(data);
			}
			return DionResponse.NotFound("Không có dữ liệu.", id);
		}

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdatePostDTO obj)
        {
			var listErrors = new List<string>();
			var isNameExist = await postRepository.IsNameExistInSameCategoryAsync(obj.Id, obj.PostCategoryId,obj.Name.Trim());
			if (isNameExist)
			{
				listErrors.Add("Tên đã tồn tại.");
			}
			if (listErrors.Count > 0)
			{
				return DionResponse.BadRequest(listErrors);
			}

            var data = await postRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }

            if (obj.IsPublish && obj.IsPublish != data.IsPublish)
            {
                data.PublishedTime = DateTime.Now;
            }
            if (obj.IsApproved && obj.IsApproved != data.IsApproved)
            {
                data.ApprovalDate = DateTime.Now;
            }



            var updateObj = mapper.Map(obj, data);

			updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;
			updateObj.Overview = updateObj.Overview.ToEscape();
            updateObj.Name = updateObj.Name.ToEscape();
            updateObj.AuthorId = SystemConstants.AUTHOR_ADMIN_ID;
            updateObj.IsApproved = obj.IsApproved;
            updateObj.IsPublish = obj.IsPublish;
            updateObj.PostLayoutId = SystemConstants.POST_LAYOUT_ID_DEFAULT;
            updateObj.PublishedTime = obj.PublishedTime;
			updateObj.Search = updateObj.Name.ToLower().RemoveVietnamese() + "_" + (updateObj.IsApproved ? "duyet" : "chua duyet") + "_" + updateObj.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss");

			
            using (var database = await postRepository.BeginTransactionAsync())
            {
                try
                {
                    await postRepository.UpdateAsync(updateObj);
                    await postRepository.SaveChangesAsync();

					var detailPost = await postRepository.DetailPostByIdAsync(updateObj.Id);

                    if (!String.IsNullOrEmpty(obj.TagIds))
                    {
                        var listPostTagInDb = await postTagRepository.ListByPostId(updateObj.Id);
                        var listIdTagOfPostInDb = listPostTagInDb.Select(s => s.TagId).ToList();

                        var listPostTagAdd = new List<PostTag>();
                        var tagId = obj.TagIds.Split(" ").Select(Int32.Parse).ToList();

                        listPostTagAdd.AddRange(tagId.Where(s=>!listIdTagOfPostInDb.Contains(s)).Select(s => new PostTag()
                        {
                            TagId = s,
                            Id = 0,
                            Active = true,
                            CreatedTime = DateTime.Now,
                            PostId = updateObj.Id,
                        }));


                        var listPostTagIdDelete = listPostTagInDb.Where(s=>!tagId.Contains(s.TagId)).Select(s=>s.Id).ToList();

                        await postTagRepository.HardDeleteListAsync(listPostTagIdDelete);
                        await postTagRepository.SaveChangesAsync();


                        await postTagRepository.CreateListAsync(listPostTagAdd);
                        await postTagRepository.SaveChangesAsync();

                        await postRepository.EndTransactionAsync();
                        return DionResponse.Success("Cập nhật bài viết thành công.");
                    }
                    else
                    {
                        //xoa het post tag
                        if (detailPost.ListTag.Count>0)
                        {
                            var listPostTagDelete = await postTagRepository.ListByPostId(updateObj.Id);
                            var listIdPostTagDelete = listPostTagDelete.Select(s => s.Id).ToList();

                            await postTagRepository.HardDeleteListAsync(listIdPostTagDelete);
                            await postTagRepository.SaveChangesAsync();

                        }

                        await postRepository.EndTransactionAsync();
                        return DionResponse.Success("Cập nhật bài viết thành công.");
                    }

                }
                catch (Exception e)
                {
					logger.LogError(e, $"Failed to update post: {obj}");
					await postRepository.RollbackTransactionAsync();
                    return DionResponse.Error("Cập nhật bài viết không thành công.");
                }
            }

		}

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdatePostDTO> obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 21/08/2023
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<DionResponse> ListPostHomePageAsync(PostParameters parameter)
        {
            var data = await postRepository.ListPostHomePageAsync(parameter);
            if (data.DataSource == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 21/08/2023
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> LoadDataFilterPostHomePageAsync()
        {
            var data = await postRepository.LoadDataFilterPostHomePageAsync();
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 11/09/2023
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<PostCategoryDTO> GetCategoryAsync(int categoryId)
        {
            var data = await postCategoryRepository.GetByIdAsync(categoryId);
            var result = mapper.Map<PostCategoryDTO>(data);
            return result;
        }
        public async Task<TagDTO> GetTagAsync(int tagId)
        {
            var data = await tagRepository.GetByIdAsync(tagId);
            var result = mapper.Map<TagDTO>(data);
            return result;
        }

    }
}
