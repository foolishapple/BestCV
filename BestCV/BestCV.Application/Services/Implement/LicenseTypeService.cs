using AutoMapper;
using BestCV.Application.Models.LicenseType;
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
    public class LicenseTypeService : ILicenseTypeService
    {
        private readonly ILicenseTypeRepository licenseTypeRepository;
        private readonly IMapper mapper;
        public LicenseTypeService(ILicenseTypeRepository _licenseTypeRepository, IMapper _mapper)
        {
            licenseTypeRepository = _licenseTypeRepository;
            mapper = _mapper;
        }

        public async  Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await licenseTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var model = mapper.Map<LicenseTypeDTO>(data);
            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> CreateAsync(InsertLicenseTypeDTO obj)
        {
            List<string> listError = new List<string>();
            var isNameExist = await licenseTypeRepository.IsNameExisAsync(obj.Name, 0);
            if (isNameExist)
            {
                listError.Add("Tên đã tồn tại");
            }
            if (listError.Count > 0)
            {
                return BestCVResponse.BadRequest(listError);
            }

            var model = mapper.Map<LicenseType>(obj);
            model.Id = 0;
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await licenseTypeRepository.CreateAsync(model);

            await licenseTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateLicenseTypeDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await licenseTypeRepository.IsNameExisAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var licenseType = await licenseTypeRepository.GetByIdAsync(obj.Id);
            if (licenseType == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var model = mapper.Map(obj, licenseType);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await licenseTypeRepository.UpdateAsync(model);
            await licenseTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var result = await licenseTypeRepository.SoftDeleteAsync(id);
            if (!result)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", id);
            }
            await licenseTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await licenseTypeRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var model = data.Select(c => mapper.Map<LicenseTypeDTO>(c));
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateLicenseTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertLicenseTypeDTO> objs)
        {
            throw new NotImplementedException();
        }
    }
}
