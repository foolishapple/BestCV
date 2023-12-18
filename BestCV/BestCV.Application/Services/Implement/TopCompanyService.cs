using AutoMapper;
using BestCV.Application.Models.TopCompany;
using BestCV.Application.Models.TopJobExtra;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class TopCompanyService : ITopCompanyService
    {
        private readonly ITopCompanyRepository topCompanyRepository;
        private readonly ILogger<ITopCompanyService> logger;
        private readonly IMapper mapper;
        public TopCompanyService(ITopCompanyRepository _topCompanyRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            topCompanyRepository = _topCompanyRepository;
            logger = loggerFactory.CreateLogger<ITopCompanyService>();
            mapper = _mapper;
        }


        public async Task<BestCVResponse> CreateAsync(InsertTopCompanyDTO obj)
        {
            var data = mapper.Map<TopCompany>(obj);
            var res = await topCompanyRepository.CheckOrderSort(0, obj.OrderSort);
            if (res)
            {
               var subOrderSort =  await topCompanyRepository.MaxOrderSort(obj.OrderSort);
               data.SubOrderSort = subOrderSort +1 ;
            }
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            var error = await Validate(data);
            if (error.Count > 0)
            {
                return BestCVResponse.BadRequest(error);
            }
            await topCompanyRepository.CreateAsync(data);
            await topCompanyRepository.SaveChangesAsync();

            return BestCVResponse.Success(data);
        }


        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertTopCompanyDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await topCompanyRepository.FindByConditionAsync(c => c.Active && c.Company.Active);
            if(data == null || data.Count == 0)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<TopCompanyDTO>>(data);
            return BestCVResponse.Success(result);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await topCompanyRepository.GetByIdAsync(id);
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<TopCompanyDTO>(data);
            return BestCVResponse.Success(result);
        }


        public async Task<List<SelectListItem>> ListCompanySelected()
        {
            return await topCompanyRepository.ListCompanySelected();
        }


        public async Task<BestCVResponse> ListTopCompanyShowOnHomePageAsync()
        {
            var result = await topCompanyRepository.ListTopCompanyShowOnHomePageAsync();
            if(result == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", result);
            }
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> ListCompany()
        {
            var data = await topCompanyRepository.ListTopCompany();
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await topCompanyRepository.SoftDeleteAsync(id);
            if (data)
            {
                await topCompanyRepository.SaveChangesAsync();
                return BestCVResponse.Success();
            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateTopCompanyDTO obj)
        {
            var topCompany = await topCompanyRepository.GetByIdAsync(obj.Id);
            if (topCompany.OrderSort != obj.OrderSort)
            {
                var subOrderSort = await topCompanyRepository.MaxOrderSort(obj.OrderSort);
                obj.SubOrderSort = subOrderSort + 1;
            }
            if (topCompany == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            

            topCompany = mapper.Map(obj, topCompany); // Cập nhật trực tiếp topCompany từ obj
            var error = await Validate(topCompany);
            if (error.Count > 0)
            {
                return BestCVResponse.BadRequest(error);
            }
            await topCompanyRepository.UpdateAsync(topCompany);
            await topCompanyRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        private async Task<List<string>> Validate(TopCompany obj)
        {
            List<string> errors = new();
            if (await topCompanyRepository.IsCompanyIdExist(obj.Id, obj.CompanyId))
            {
                errors.Add($"Tên công ty đã tồn tại.");
            }
            return errors;
        }
        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateTopCompanyDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> ChangeOrderSort(ChangeTopCompanyDTO model)
        {
            var orderSortUp = mapper.Map<TopCompany>(model.SlideUp);
            var orderSortDown = mapper.Map<TopCompany>(model.SlideDown);
            var listUpdate = new List<TopCompany>();
            var orderTemp = orderSortUp.OrderSort;
            orderSortUp.OrderSort = orderSortDown.OrderSort;
            orderSortDown.OrderSort = orderTemp;
            var subOrderTemp = orderSortUp.SubOrderSort;
            orderSortUp.SubOrderSort = orderSortDown.SubOrderSort;
            orderSortDown.SubOrderSort = subOrderTemp;
            listUpdate.Add(orderSortUp);
            listUpdate.Add(orderSortDown);
            await topCompanyRepository.ChangeOrderSort(listUpdate);
            await topCompanyRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }
    }
}
