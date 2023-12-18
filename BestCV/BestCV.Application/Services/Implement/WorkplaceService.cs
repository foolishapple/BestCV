using AutoMapper;
using BestCV.Application.Models.AdminAccounts;
using BestCV.Application.Models.WorkPlace;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class WorkplaceService : IWorkplaceService
    {
        private readonly IWorkplaceRepository _workplaceRepository;
        private readonly ILogger<WorkplaceService> _logger;
        private readonly IMapper _mapper;

        public WorkplaceService(IWorkplaceRepository workplaceRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _workplaceRepository = workplaceRepository;
            _logger = loggerFactory.CreateLogger<WorkplaceService>();
            _mapper = mapper;
        }


        public async Task<BestCVResponse> CreateAsync(InsertWorkplaceDTO obj)
        {
            var workplace = _mapper.Map<WorkPlace>(obj);
            await _workplaceRepository.CreateAsync(workplace);
            return BestCVResponse.Success(obj);
        }


        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertWorkplaceDTO> objs)
        {
            var workplaces = objs.Select(c => _mapper.Map<WorkPlace>(c));
            await _workplaceRepository.CreateListAsync(workplaces);
            return BestCVResponse.Success(objs);
        }

 
        public async Task<BestCVResponse> GetAllAsync()
        {
            var listWorkplace = await _workplaceRepository.GetAllAsync();
            var listWorkplaceDTO = _mapper.Map<List<WorkplaceDTO>>(listWorkplace);
            return BestCVResponse.Success(listWorkplaceDTO);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var workplace = await _workplaceRepository.GetByIdAsync(id);
            var workplaceDTO = _mapper.Map<WorkplaceDTO>(workplace);
            return BestCVResponse.Success(workplaceDTO);
        }

        public Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateWorkplaceDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateWorkplaceDTO> obj)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> GetProvinceDataAsync()
        {
            var listWorkplaceOld = await _workplaceRepository.GetAllAsync();
            if (listWorkplaceOld.Count != 0)
            {
                return false;
            }
            using (HttpClient client = new())
            {
                List<CityDTO> listCity = await client.GetRequestAsync<List<CityDTO>>(WorkplaceConstants.PROVINCE_API);

                if (listCity != null && listCity.Count != 0)
                {
                    foreach (CityDTO city in listCity)
                    {
                        WorkPlace workplaceForCity = new()
                        {
                            Active = true,
                            Name = city.name,
                            Description = city.name,
                            CreatedTime = DateTime.Now,
                            ParentId = null,
                            Code = city.code,
                            DivisionType = city.division_type,
                            CodeName = city.codename,
                            PhoneCode = city.phone_code,
                            ProvinceCode = null
                        };
                        await _workplaceRepository.CreateAsync(workplaceForCity);
                        await _workplaceRepository.SaveChangesAsync();

                        List<WorkPlace> listWorkplace = new();
                        foreach (DistrictDTO district in city.districts)
                        {
                            WorkPlace workplaceForDistrict = new()
                            {
                                Active = true,
                                Name = district.name,
                                Description = district.name + " - " + city.name,
                                CreatedTime = DateTime.Now,
                                ParentId = workplaceForCity.Id,
                                Code = district.code,
                                DivisionType = district.division_type,
                                CodeName = district.codename,
                                PhoneCode = null,
                                ProvinceCode = district.province_code
                            };
                            listWorkplace.Add(workplaceForDistrict);
                        }
                        await _workplaceRepository.CreateListAsync(listWorkplace);
                    }

                    await _workplaceRepository.SaveChangesAsync();

                    return true;
                }
                return false;            
            }
        }

 
        public async Task<BestCVResponse> GetListDistrictByCityIdAsync(int cityId)
        {
            var listWorkplace = await _workplaceRepository.GetListDistrictByCityIdAsync(cityId);
            var listWorkplaceDTO = _mapper.Map<List<WorkplaceDTO>>(listWorkplace);
            return BestCVResponse.Success(listWorkplaceDTO);
        }


        public async Task<BestCVResponse> GetListCityAsync()
        {
            var listWorkplace = await _workplaceRepository.GetListCityAsync();
            var listWorkplaceDTO = _mapper.Map<List<WorkplaceDTO>>(listWorkplace);
            return BestCVResponse.Success(listWorkplaceDTO);
        }
    }
}
