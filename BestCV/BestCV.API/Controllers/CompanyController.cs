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
        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 02/08/2023
        /// Description : add company
        /// </summary>
        /// <param"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 02/08/2023
        /// Description : Detail by id
        /// </summary>
        /// <param"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 02/08/2023
        /// Description : Detail by id
        /// </summary>
        /// <param"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 09/08/2023
        /// Description : aggregates Detail by id
        /// </summary>
        /// <param"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 02/08/2023
        /// Description : Update profile company
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 07/08/2023
        /// Description : Load danh sách tổ chức tuyển dụng admin server side
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 07/08/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Author : ThanhnD
        /// CreatedTime : 07/08/2023
        /// </summary>
        /// <param name="fileGuid"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 07/08/2023
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
