using BestCV.Application.Models.VoucherTypes;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/voucher-type")]
    public class VoucherTypeController : BaseController
    {
        private readonly IVoucherTypeService _service;
        private readonly ILogger _logger;
        public VoucherTypeController(IVoucherTypeService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<VoucherTypeController>();
        }
 
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await _service.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all voucher type");
                return BadRequest();
            }
        }
   
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required(ErrorMessage = "Mã không được để trống")] int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get voucher type detail");
                return BadRequest();
            }
        }
 
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertVoucherTypeDTO obj)
        {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new voucher type");
                return BadRequest();
            }
        }
  
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InsertVoucherTypeDTO obj)
        {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update new voucher type");
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Mã không được để trống")] int id)
        {
            try
            {
                var response = await _service.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to soft delete voucher type");
                return BadRequest();
            }
        }
    }
}
