using AutoMapper;
using BestCV.Application.Models.Occupation;
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
    public class OccupationService : IOccupationService
    {
        private readonly IOccupationRepository occupationRepository;
        private readonly IMapper mapper;
        public OccupationService(IOccupationRepository _occupationRepository, IMapper _mapper)
        {
            occupationRepository = _occupationRepository;
            mapper = _mapper;
        }

        public async Task<BestCVResponse> CreateAsync(InsertOccupationDTO obj)
        {
            List<string> listError = new List<string>();
            var isNameExist = await occupationRepository.IsNameExisAsync(obj.Name, 0);
            if (isNameExist)
            {
                listError.Add("Tên đã tồn tại");
            }
            if (listError.Count > 0)
            {
                return BestCVResponse.BadRequest(listError);
            }    

            var model = mapper.Map<Occupation>(obj);
            model.Id = 0;
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await occupationRepository.CreateAsync(model);
            
            await occupationRepository.SaveChangesAsync();
			return BestCVResponse.Success(model);
		}


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await occupationRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var model = data.Select(c => mapper.Map<OccupationDTO>(c));
            return BestCVResponse.Success(model);
        }


		public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await occupationRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var model = mapper.Map<OccupationDTO>(data);
            return BestCVResponse.Success(model);
        }


		public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var result = await occupationRepository.SoftDeleteAsync(id);
            if (!result)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", id);
			}
			await occupationRepository.SaveChangesAsync();
			return BestCVResponse.Success();
        }


		public async Task<BestCVResponse> UpdateAsync(UpdateOccupationDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await occupationRepository.IsNameExisAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var occupation = await occupationRepository.GetByIdAsync(obj.Id);
            if (occupation == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var model = mapper.Map(obj, occupation);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await occupationRepository.UpdateAsync(model);
            await occupationRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateOccupationDTO> obj)
        {
            throw new NotImplementedException();
        }
		public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
		{
			throw new NotImplementedException();
		}
		public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertOccupationDTO> objs)
		{
			throw new NotImplementedException();
		}
	}
}
