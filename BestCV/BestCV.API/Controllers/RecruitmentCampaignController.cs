using BestCV.API.Utilities;
using BestCV.Application.Models.RecruitmentCampaigns;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Aggregates.RecruitmentCampaigns;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
    [Route("api/recruitment-campaign")]
    [ApiController]
    public class RecruitmentCampaignController : ControllerBase
    {
        private readonly IRecruitmentCampaignService _service;
        private readonly ILogger _logger;
        public RecruitmentCampaignController(IRecruitmentCampaignService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<RecruitmentCampaignController>();
        }
        #region CRUD

        [HttpPost("add-to-employer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddToEmployer([FromBody] InsertRecruitmentCampaignDTO obj)
        {
            try
            {
                var userId = this.GetLoggedInUserId();
                obj.EmployerId = userId;
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new recruitment campagin");
                return BadRequest();
            }
        }

        [HttpPut("update-to-employer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateToEmployer([FromBody] UpdateRecruitmentCampaignDTO obj)
        {
            try
            {
                var response = await _service.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update  recruitment campagin");
                return BadRequest();
            }
        }

        [HttpPut("change-approved-to-employer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangeApprovedToEmployer([FromBody] ChangeApproveRecruitmentCampaignDTO obj)
        {
            try
            {
                var response = await _service.ChangeApproved(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to change approved to employer recruitment campagin");
                return BadRequest();
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(long id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get recruitment campagin detail");
                return BadRequest();
            }
        }
        #endregion

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("list-to-employer")]
        public async Task<IActionResult> ListToEmployer()
        {
            try
            {
                var userId = this.GetLoggedInUserId();
                var response = await _service.ListToEmployer(userId);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list Recruitment Campaign to employer");
                return BadRequest();
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("list-dt-paging")]
        public async Task<IActionResult> ListDTPaging([FromBody] DTRecruitmentCampaignParameter parameters)
        {
            try
            {
                var userId = this.GetLoggedInUserId();
                parameters.EmployerId = userId;
                var response = await _service.ListDTPaging(parameters);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list recruitment campaing datatables paging");
                return BadRequest();
            }
        }

        [HttpGet("list-opening-to-employer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListOpeningToEmployer()
        {
            try
            {
                var userId = this.GetLoggedInUserId();
                var result = await _service.ListOpenedByEmployer(userId);
                return Ok(result);
            }
            catch (Exception e) 
            {
                _logger.LogError(e,"Failed to get list recruit ment campagin opened by userId");
                return BadRequest();
            }
        }
    }
}
