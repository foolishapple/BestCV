using AutoMapper;
using BestCV.Application.Models.Slides;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class SlideService : ISlideService
    {
        private readonly ISlideRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public SlideService(ISlideRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<SlideService>();
            _mapper = mapper;
        }

        public async Task<BestCVResponse> CreateAsync(InsertSlideDTO obj)
        {
            List<string> listErrors = new List<string>();
            
            var item = _mapper.Map<Slide>(obj);
            var checkName = await _repository.NameIsExisted(obj.Name, 0);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            
            
                var suborder = await _repository.MaxSubSort(obj.CandidateOrderSort);
                item.SubOrderSort = suborder + 1 ;
                
                
            
            
            item.Active = true;
            item.CreatedTime = DateTime.Now;
            
            
            await _repository.CreateAsync(item);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertSlideDTO> objs)
        {
            List<string> errors = new();
            var items = objs.Select(c => _mapper.Map<Slide>(c));
            foreach (var item in items)
            {
                item.Active = true;
                item.CreatedTime = DateTime.Now;
                errors.AddRange(await Validate(item));
            }
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.CreateListAsync(items);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(objs);
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _repository.GetAllSort();
            return BestCVResponse.Success(data);
        }
   
        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found slide by id: {id}");
            }
            return BestCVResponse.Success(item);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var result = await _repository.SoftDeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return BestCVResponse.Success(id);
            }
            throw new Exception($"Not found slide by id: {id}");
        }

        public async Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            var result = await _repository.SoftDeleteListAsync(objs);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return BestCVResponse.Success(objs);
            }
            throw new Exception($"Failed to soft delete list slide by list slide id");
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateSlideDTO obj)
        {
            var item = await _repository.GetByIdAsync(obj.Id);
           
            
            if (item.CandidateOrderSort != obj.CandidateOrderSort)
            {
                
                    var suborder = await _repository.MaxSubSort(obj.CandidateOrderSort);
                    obj.SubOrderSort = suborder + 1;
                

            }
           

            if (item == null)
            {
                throw new Exception($"Not found slide by id: {obj.Id}");
            }
            item = _mapper.Map(obj, item);
            var errors = await Validate(item);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            
            await _repository.UpdateAsync(item);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateSlideDTO> obj)
        {
            List<Slide> updateItems = new();
            List<string> errors = new();
            foreach (var item in obj)
            {
                var model = await _repository.GetByIdAsync(item.Id);
                if (model == null)
                {
                    throw new Exception($"Not found slide by id: {item.Id}");
                }
                model = _mapper.Map(item, model);
                updateItems.Add(model);
                errors.AddRange(await Validate(model));
            }
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.UpdateListAsync(updateItems);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        private async Task<List<string>> Validate(Slide obj)
        {
            List<string> errors = new();
            if (await _repository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add($"Tên {obj.Name} đã tồn tại.");
            }
            return errors;
        }

        public async Task<bool> ChangeOrderSlide(ChangeSlideDTO model)
        {
            var courseLectueUp = _mapper.Map<Slide>(model.SlideUp);
            var courseLectureDown = _mapper.Map<Slide>(model.SlideDown);
            var listUpdate = new List<Slide>();
            var orderTemp = courseLectueUp.CandidateOrderSort;
            courseLectueUp.CandidateOrderSort = courseLectureDown.CandidateOrderSort;
            courseLectureDown.CandidateOrderSort = orderTemp;
            var subOrderTemp = courseLectueUp.SubOrderSort;
            courseLectueUp.SubOrderSort = courseLectureDown.SubOrderSort;
            courseLectureDown.SubOrderSort = subOrderTemp;
            listUpdate.Add(courseLectueUp);
            listUpdate.Add(courseLectureDown);
            var isSuccess = await _repository.ChangeOrderSort(listUpdate);
            if (isSuccess)
            {
                return true;
            }
            return false;
        }

        public async Task<BestCVResponse> ListSlideShowonHomepage()
        {
            var data = await _repository.ListSlideShowonHomepage();
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }
    }
}
