using BestCV.Application.Models.TopCompany;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using BestCV.Core.Entities;
using BestCV.Application.Models.TopJobExtra;

namespace BestCV.API.Controllers
{
    [Route("api/top-company")]
    [ApiController]
    public class TopCompanyController : BaseController
    {
        private readonly ITopCompanyService service;
        private readonly ILogger<TopCompanyController> logger;
        public TopCompanyController(ITopCompanyService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<TopCompanyController>();
        }

        #region CRUD

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var reponse = await service.GetByIdAsync(id);
                return Ok(reponse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get top company by id: {id}");
                return BadRequest();
            }
        }

   
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertTopCompanyDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add top company");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTopCompanyDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update top company");
                return BadRequest();
            }
        }

 
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var data = await service.SoftDeleteAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to soft delete top company");
                return BadRequest();
            }
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await service.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list top company");
                return BadRequest();
            }
        }

        [HttpGet("list-by-orderSort")]
        public async Task<IActionResult> GetListbyOrderSort()
        {
            try
            {
                var data = await service.ListCompany();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list top company");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

        [HttpGet("list-top-company")]
        public async Task<IActionResult> GetListTopCompanyShowOnHomePage()
        {
            try
            {
                var data = await service.ListTopCompanyShowOnHomePageAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list top company");
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("change-order-slide")]
        public async Task<IActionResult> ChangeOrderSort([FromBody] ChangeTopCompanyDTO model)
        {
            try
            {
                var isSuccess = await service.ChangeOrderSort(model);
                return Ok(BestCVResponse.Success(isSuccess));

            }
            catch (Exception ex)
            {
                logger.LogError("Fail when change order sort", ex);
                return BadRequest();
            }
        }


		[HttpGet("List-Company-Selected")]
        public async Task<IActionResult> ListCompanySelected()
        {
            try
            {
                var data = await service.ListCompanySelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected company");
                return BadRequest();
            }
        }
        #endregion
    }
}
