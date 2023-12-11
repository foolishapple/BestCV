using AutoMapper;
using BestCV.Application.Models.MustBeInterestedCompany;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class MustBeInterestedCompanyService : IMustBeInterestedCompanyService
    {
        private readonly IMustBeInterestedCompanyRepository repository;
        private readonly ILogger<IMustBeInterestedCompanyService> logger;
        private readonly IMapper mapper;
        public MustBeInterestedCompanyService(IMustBeInterestedCompanyRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<IMustBeInterestedCompanyService>();
            mapper = _mapper;
        }

        public async Task<DionResponse> CreateAsync(InsertMustBeInterestedCompanyDTO obj)
        {
            // Kiểm tra xem JobId đã tồn tại trong JobSuitable hay chưa
            var isJobIdExist = await repository.IsJobIdExistAsync(obj.CompanyId);
            if (isJobIdExist)
            {
                var errorList = new List<string>
            {
                "Tên công việc đã tồn tại."
            };
                return DionResponse.BadRequest(errorList);
            }
            var mustBeInterestedCompany = mapper.Map<MustBeInterestedCompany>(obj);
            mustBeInterestedCompany.Active = true;
            mustBeInterestedCompany.CreatedTime = DateTime.Now;
            var listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await repository.CreateAsync(mustBeInterestedCompany);
            await repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertMustBeInterestedCompanyDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await repository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<MustBeInterestedCompanyDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<MustBeInterestedCompanyDTO>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> ListAggregatesAsync()
        {
            var result = await repository.ListAggregatesAsync();
            if (result == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", result);
            }
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> ListCompanyInterestedOnDetailJob()
        {
            var data = await repository.ListCompanyInterestedOnDetailJob();
            if(data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<List<SelectListItem>> ListCompanySelected()
        {
            return await repository.ListCompanySelected();
        }

        public async Task<DionResponse> SoftDeleteAsync(long id)
        {
            var data = await repository.SoftDeleteAsync(id);
            if (data)
            {
                await repository.SaveChangesAsync();
                return DionResponse.Success();
            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateMustBeInterestedCompanyDTO obj)
        {
            var mustBeInterestedCompany = await repository.GetByIdAsync(obj.Id);
            if (mustBeInterestedCompany == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            // Kiểm tra chỉ khi JobId thay đổi
            if (obj.CompanyId != mustBeInterestedCompany.CompanyId)
            {
                var isJobIdExist = await repository.IsJobIdExistAsync(obj.CompanyId);
                if (isJobIdExist)
                {
                    var errorList = new List<string>
                    {
                        "Tên công việc đã tồn tại."
                    };
                    return DionResponse.BadRequest(errorList);
                }
            }
            var updateMustBeInterestedCompany = mapper.Map(obj, mustBeInterestedCompany);
            var listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await repository.UpdateAsync(updateMustBeInterestedCompany);
            await repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateMustBeInterestedCompanyDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
