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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Create new slide
        /// </summary>
        /// <param name="obj">slide DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertSlideDTO obj)
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
                return DionResponse.BadRequest(listErrors);
            }
            
            
                var suborder = await _repository.MaxSubSort(obj.CandidateOrderSort);
                item.SubOrderSort = suborder + 1 ;
                
                
            
            
            item.Active = true;
            item.CreatedTime = DateTime.Now;
            
            
            await _repository.CreateAsync(item);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Create new list slide
        /// </summary>
        /// <param name="objs">list slide DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertSlideDTO> objs)
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
                return DionResponse.BadRequest(errors);
            }
            await _repository.CreateListAsync(items);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(objs);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Get list all slide
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await _repository.GetAllSort();
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Get slide by id
        /// </summary>
        /// <param name="id">slide id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found slide by id: {id}");
            }
            return DionResponse.Success(item);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete slide by id
        /// </summary>
        /// <param name="id">slide by id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var result = await _repository.SoftDeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return DionResponse.Success(id);
            }
            throw new Exception($"Not found slide by id: {id}");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete list slide by list slide id
        /// </summary>
        /// <param name="objs">list slide id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Failed delete</exception>
        public async Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            var result = await _repository.SoftDeleteListAsync(objs);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return DionResponse.Success(objs);
            }
            throw new Exception($"Failed to soft delete list slide by list slide id");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Update slide
        /// </summary>
        /// <param name="obj">update slide DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateAsync(UpdateSlideDTO obj)
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
                return DionResponse.BadRequest(errors);
            }
            
            await _repository.UpdateAsync(item);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: update list slide
        /// </summary>
        /// <param name="obj">list slide DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateSlideDTO> obj)
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
                return DionResponse.BadRequest(errors);
            }
            await _repository.UpdateListAsync(updateItems);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Validate to slide
        /// </summary>
        /// <param name="obj">slide object</param>
        /// <returns></returns>
        private async Task<List<string>> Validate(Slide obj)
        {
            List<string> errors = new();
            if (await _repository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add($"Tên {obj.Name} đã tồn tại.");
            }
            return errors;
        }
        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 10/09/2023
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        public async Task<DionResponse> ListSlideShowonHomepage()
        {
            var data = await _repository.ListSlideShowonHomepage();
            if(data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }
    }
}
