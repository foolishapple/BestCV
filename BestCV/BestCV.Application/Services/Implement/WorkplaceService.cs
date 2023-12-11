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

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 27/07/2023
        /// Description: Tạo mới địa điểm làm việc
        /// </summary>
        /// <param name="obj">InsertWorkplaceDTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertWorkplaceDTO obj)
        {
            var workplace = _mapper.Map<WorkPlace>(obj);
            await _workplaceRepository.CreateAsync(workplace);
            return DionResponse.Success(obj);
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 27/07/2023
        /// Description: Tạo mới danh sách địa điểm làm việc
        /// </summary>
        /// <param name="objs">IEnumerable<InsertWorkplaceDTO></param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertWorkplaceDTO> objs)
        {
            var workplaces = objs.Select(c => _mapper.Map<WorkPlace>(c));
            await _workplaceRepository.CreateListAsync(workplaces);
            return DionResponse.Success(objs);
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 27/07/2023
        /// Description: Lấy tất cả dữ tỉnh thành quận huyện Việt Nam
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var listWorkplace = await _workplaceRepository.GetAllAsync();
            var listWorkplaceDTO = _mapper.Map<List<WorkplaceDTO>>(listWorkplace);
            return DionResponse.Success(listWorkplaceDTO);
        }

        /// <summary>
        /// Author: Daniel
        /// Description: Lấy thông tin chi tiết địa chỉ làm việc
        /// </summary>
        /// <param name="id">ID địa chỉ làm việc</param>
        /// <returns>WorkPlace</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var workplace = await _workplaceRepository.GetByIdAsync(id);
            var workplaceDTO = _mapper.Map<WorkplaceDTO>(workplace);
            return DionResponse.Success(workplaceDTO);
        }

        public Task<DionResponse> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateAsync(UpdateWorkplaceDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateWorkplaceDTO> obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 27/07/2023
        /// Description: Lấy dữ liệu hành chính VN đưa vào DB
        /// Updater: Daniel
        /// UpdatedDate: 31/07/2023
        /// Description: Sửa Description Workplace khi thêm dữ liệu vào DB
        /// </summary>
        /// <returns>Lấy dữ liệu thành công hay thất bại</returns>
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

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 27/07/2023
        /// Description: Lấy dánh sách quận huyện theo ID tỉnh thành
        /// </summary>
        /// <param name="cityId">ID tỉnh thành</param>
        /// <returns>Danh sách quận huyện</returns>
        public async Task<DionResponse> GetListDistrictByCityIdAsync(int cityId)
        {
            var listWorkplace = await _workplaceRepository.GetListDistrictByCityIdAsync(cityId);
            var listWorkplaceDTO = _mapper.Map<List<WorkplaceDTO>>(listWorkplace);
            return DionResponse.Success(listWorkplaceDTO);
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 27/07/2023
        /// Description: Lấy danh sách tất cả tỉnh thành
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetListCityAsync()
        {
            var listWorkplace = await _workplaceRepository.GetListCityAsync();
            var listWorkplaceDTO = _mapper.Map<List<WorkplaceDTO>>(listWorkplace);
            return DionResponse.Success(listWorkplaceDTO);
        }
    }
}
