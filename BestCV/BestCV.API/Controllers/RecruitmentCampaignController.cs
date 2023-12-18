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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: API POST add new recruitment campagin to employer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: API PUT update recruitment campagin to employer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: API PUT update recruitment campagin to employer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: API GET get recruitment campagin detail by id
        /// </summary>
        /// <param name="id">recruitment campaign id</param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 16/08/2023
        /// Description: API GET get list Recruitment Campaign to employer 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 21/08/2023
        /// Description: API POST get list recruitment campaign datatables paging
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 13/09/2023
        /// Description: API Get list recruitment campagin opened to eployer
        /// </summary>
        /// <returns></returns>
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
