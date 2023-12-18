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

        public async Task<BestCVResponse> CreateAsync(InsertSkillDTO obj)
        {
            List<string> listError = new List<string>();
            var isNameExist = await skillRepository.IsNameExisAsync(obj.Name, 0);
            if (isNameExist)
            {
                listError.Add("Tên đã tồn tại");
            }
            if (listError.Count > 0)
            {
                return BestCVResponse.BadRequest(listError);
            }

            var model = mapper.Map<Skill>(obj);
            model.Id = 0;
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await skillRepository.CreateAsync(model);

            await skillRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertSkillDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await skillRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var model = data.Select(c => mapper.Map<SkillDTO>(c));
            return BestCVResponse.Success(model);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await skillRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var model = mapper.Map<SkillDTO>(data);
            return BestCVResponse.Success(model);
        }


        public async Task<BestCVResponse> searchSkills(Select2Aggregates select2Aggregates)
        {
            if(select2Aggregates != null
                && !string.IsNullOrEmpty(select2Aggregates.SearchString)
                && select2Aggregates.PageLimit.HasValue
                && select2Aggregates.PageLimit > 0)
            {
                var data = await skillRepository.searchSkills(select2Aggregates);
                return BestCVResponse.Success(data);
            }
            else
            {
                return BestCVResponse.BadRequest("Không có dữ liệu");
            }
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var result = await skillRepository.SoftDeleteAsync(id);
            if (!result)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", id);
            }
            await skillRepository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

 
        public async Task<BestCVResponse> UpdateAsync(UpdateSkillDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await skillRepository.IsNameExisAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var skill = await skillRepository.GetByIdAsync(obj.Id);
            if (skill == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var model = mapper.Map(obj, skill);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await skillRepository.UpdateAsync(model);
            await skillRepository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public Task<BestCVResponse> UpdateAsync(SkillDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<SkillDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
