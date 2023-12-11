using AutoMapper;
using BestCV.Application.Models.PostType;
using BestCV.Application.Models.Tag;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Tag;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly ILogger<ITagService> logger;
        private readonly IMapper mapper;

        public TagService(ITagRepository tagRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.logger = loggerFactory.CreateLogger<ITagService>();
            this.mapper = mapper;
        }

        public async Task<DionResponse> AddTagForJobAsync(InsertTagDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await tagRepository.IsNameTypeJobExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var newObj = mapper.Map<Tag>(obj);
            newObj.Id = 0;
            newObj.TagTypeId = TagTypeId.JOB;

            await tagRepository.CreateAsync(newObj);
            await tagRepository.SaveChangesAsync();
            return DionResponse.Success(newObj);
        }

        public async Task<DionResponse> AddTagForPostAsync(InsertTagDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await tagRepository.IsNameTypePostExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var newObj = mapper.Map<Tag>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.TagTypeId = TagTypeId.POST;

            await tagRepository.CreateAsync(newObj);
            await tagRepository.SaveChangesAsync();
            return DionResponse.Success(newObj);
        }

        public async Task<DionResponse> CreateAsync(InsertTagDTO obj)
        {
            List<string> listError = new List<string>();
            var isNameExist = await tagRepository.IsNameExisAsync(obj.Name, 0);
            if (isNameExist)
            {
                listError.Add("Tên đã tồn tại");
            }
            if (listError.Count > 0)
            {
                return DionResponse.BadRequest(listError);
            }

            var model = mapper.Map<Tag>(obj);
            model.Id = 0;
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            await tagRepository.CreateAsync(model);

            await tagRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertTagDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await tagRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<TagDTO>>(data);
            return DionResponse.Success(temp);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await tagRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var temp = mapper.Map<TagDTO>(data);
            return DionResponse.Success(temp);
        }

        public async Task<object> ListSelectTagAsync(TagForSelect2Aggregates obj)
        {
            return await tagRepository.ListSelectTagAsync(obj);

        }

        /// <summary>
        /// Authoor: TrungHieuTr
        /// created: 13/09/2023
        /// description: get list TagAggregates
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<object> ListTagAggregatesAsync(DTParameters parameters)
        {
            return await tagRepository.ListTagAggregatesAsync(parameters);
        }

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 18.08.2023
        /// description: list tag type job
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> ListTagTypeJob()
        {
            var result = await tagRepository.ListTagTypeJob();
            return DionResponse.Success(result);
        }

        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> ListTagTypePost()
        {
            var result = await tagRepository.FindByConditionAsync(x => x.Active && x.TagTypeId == TagTypeId.POST);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var result = await tagRepository.SoftDeleteAsync(id);
            if (!result)
            {
                return DionResponse.NotFound("Không có dữ liệu", id);
            }
            await tagRepository.SaveChangesAsync();
            return DionResponse.Success();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateTagDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await tagRepository.IsNameExisAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var tag = await tagRepository.GetByIdAsync(obj.Id);
            if (tag == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var model = mapper.Map(obj, tag);
            //model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await tagRepository.UpdateAsync(model);
            await tagRepository.SaveChangesAsync();
            return DionResponse.Success(model);

        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateTagDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
