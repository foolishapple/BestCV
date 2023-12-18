using BestCV.API.Utilities;
using BestCV.Application.Models.Company;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Company;
using BestCV.Domain.Aggregates.Employer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService service;
        private readonly ILogger<CompanyController> logger;

        public CompanyController(ICompanyService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CompanyController>();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        #region CRUD     

        [HttpPost("add-profile-company")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Add([FromBody] InsertCompanyDTO obj)
        {
            obj.EmployerId = this.GetLoggedInUserId();
            try
            {
                var response = await service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get add Company for employerId: {obj.EmployerId}");
                return BadRequest();
            }
        }


        [HttpGet("detailByEmployerId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> detailByEmployerId()
        {
            var employerId = this.GetLoggedInUserId();
            try
            {
                var response = await service.GetDetailByEmnployerId(employerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get detail Company by id: {employerId}");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> detail([Required] int id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get detail Company by id: {id}");
                return BadRequest();
            }
        }


        [HttpGet("detailAggregate/{id}")]
        public async Task<IActionResult> GetDetailAggregatesById([Required] int id)
        {
            try
            {
                var response = await service.GetCompanyAggregatesById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get detail Company by id: {id}");
                return BadRequest();
            }
        }


        [HttpPut("update-profile-company")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyDTO obj)
        {
            obj.EmployerId = this.GetLoggedInUserId();
            try
            {
                var res = await service.UpdateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update Company");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources
        [HttpPost("list-company-aggregates")]
        public async Task<IActionResult> ListCompanyAgrregates(DTParameters parameters)
        {
            try
            {
                return Json(await service.ListCompanyAggregate(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load list company aggregates");
                return BadRequest();
            }
        }

        [HttpGet("detail-admin/{id}")]
        public async Task<IActionResult> DetailAdmin(int id)
        {
            try
            {
                var response = await service.DetailAdmin(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load list company aggregates");
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("download-excel")]
        public IActionResult DownloadExcel(string fileGuid, string fileName)
        {
            try
            {
                var data = service.DownloadExcel(fileGuid, fileName);
                if (data != null)
                {
                    return File(data, "application/vnd.ms-excel", fileName);
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to download Excel");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("export-excel")]
        public async Task<IActionResult> ExportExcel(List<CompanyAggregates> data)
        {
            try
            {
                var response = await service.ExportExcel(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to export excel");
                return BadRequest();
            }
        }

        [HttpPost("searching-company")]
        public async Task<IActionResult> SearchingCompany([FromBody] SearchingCompanyParameters parameters)
        {
            try
            {
                parameters.CandidateId = this.GetLoggedInUserId();
                var data = await service.SearchCompanyHomePageAsync(parameters);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to searching job");
                return BadRequest();
            }
        }
        #endregion
    }
}
