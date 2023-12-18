using AutoMapper;
using BestCV.Application.Models.SkillLevel;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
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
    public class SkillLevelService : ISkillLevelService
    {
        private readonly ISkillLevelRepository skillLevelRepository;
        private readonly IMapper mapper;
        public SkillLevelService(ISkillLevelRepository _skilllevelRepository, IMapper _mapper)
        {
            skillLevelRepository = _skilllevelRepository;
            mapper = _mapper;
        }

        public async Task<BestCVResponse> CreateAsync(InsertSkillLevelDTO obj)
        {
            List<string> listError = new List<string>();
            var isNameExist = await skillLevelRepository.IsNameExisAsync(obj.Name, 0);
            if (isNameExist)
            {
                listError.Add("Tên đã tồn tại");
            }
            if (listError.Count > 0)
            {
                return BestCVResponse.BadRequest(listError);
            }

            var model = mapper.Map<SkillLevel>(obj);
            model.Id = 0;
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await skillLevelRepository.CreateAsync(model);

            await skillLevelRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }
  
        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await skillLevelRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var model = data.Select(c => mapper.Map<SkillLevelDTO>(c));
            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await skillLevelRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var model = mapper.Map<SkillLevelDTO>(data);
            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var result = await skillLevelRepository.SoftDeleteAsync(id);
            if (!result)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", id);
            }
            await skillLevelRepository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateSkillLevelDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await skillLevelRepository.IsNameExisAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var skillLevel = await skillLevelRepository.GetByIdAsync(obj.Id);
            if (skillLevel == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var model = mapper.Map(obj, skillLevel);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await skillLevelRepository.UpdateAsync(model);
            await skillLevelRepository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateSkillLevelDTO> obj)
        {
            throw new NotImplementedException();
        }
        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertSkillLevelDTO> objs)
        {
            throw new NotImplementedException();
        }
    }
}
