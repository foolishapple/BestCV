using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/account-status")]
    [ApiController]
    public class AccountStatusController : BaseController
    {
        private readonly IAccountStatusService service;
        private readonly ILogger<AccountStatusController> logger;
        public AccountStatusController(IAccountStatusService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<AccountStatusController>();
        }

        #region CRUD
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get account status by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertAccountStatusDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add account status");
                return BadRequest();
            }
        }

 
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountStatusDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update account status");
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
                logger.LogError(ex, $"Failed to soft delete account status id");
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
                logger.LogError(ex, $"Failed to get list account status");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

        #endregion
    }
}
