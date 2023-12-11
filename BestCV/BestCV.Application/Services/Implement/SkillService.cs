using AutoMapper;
using BestCV.Application.Models.Skill;
using BestCV.Application.Models.SkillLevel;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.TopFeatureJob;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository skillRepository;
        private readonly IMapper mapper;
        public SkillService(ISkillRepository _skillRepository, IMapper _mapper)
        {
            skillRepository = _skillRepository;
            mapper = _mapper;
        }
        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertSkillDTO obj)
        {
            List<string> listError = new List<string>();
            var isNameExist = await skillRepository.IsNameExisAsync(obj.Name, 0);
            if (isNameExist)
            {
                listError.Add("Tên đã tồn tại");
            }
            if (listError.Count > 0)
            {
                return DionResponse.BadRequest(listError);
            }

            var model = mapper.Map<Skill>(obj);
            model.Id = 0;
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await skillRepository.CreateAsync(model);

            await skillRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertSkillDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await skillRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var model = data.Select(c => mapper.Map<SkillDTO>(c));
            return DionResponse.Success(model);
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await skillRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var model = mapper.Map<SkillDTO>(data);
            return DionResponse.Success(model);
        }

        /// <summary>
        /// Author: HoanNK
        /// Created: 22/8/2023
        /// </summary>
        /// <param name="select2Aggregates"></param>
        /// <returns></returns>
        public async Task<DionResponse> searchSkills(Select2Aggregates select2Aggregates)
        {
            if(select2Aggregates != null
                && !string.IsNullOrEmpty(select2Aggregates.SearchString)
                && select2Aggregates.PageLimit.HasValue
                && select2Aggregates.PageLimit > 0)
            {
                var data = await skillRepository.searchSkills(select2Aggregates);
                return DionResponse.Success(data);
            }
            else
            {
                return DionResponse.BadRequest("Không có dữ liệu");
            }
        }

        /// <summary>
        /// Author : Nam Anh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var result = await skillRepository.SoftDeleteAsync(id);
            if (!result)
            {
                return DionResponse.NotFound("Không có dữ liệu", id);
            }
            await skillRepository.SaveChangesAsync();
            return DionResponse.Success();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdateSkillDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await skillRepository.IsNameExisAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var skill = await skillRepository.GetByIdAsync(obj.Id);
            if (skill == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var model = mapper.Map(obj, skill);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await skillRepository.UpdateAsync(model);
            await skillRepository.SaveChangesAsync();
            return DionResponse.Success();
        }

        public Task<DionResponse> UpdateAsync(SkillDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<SkillDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
