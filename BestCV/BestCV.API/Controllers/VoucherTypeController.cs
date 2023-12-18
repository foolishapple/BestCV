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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API GET get list all voucher type
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API GET get voucher type detail by id
        /// </summary>
        /// <param name="id">voucher type id</param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API POST create new voucher type
        /// </summary>
        /// <param name="obj">insert voucher type DTO</param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API PUT update voucher type
        /// </summary>
        /// <param name="obj">update voucher type DTO</param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API DELETE soft delete voucher type by id
        /// </summary>
        /// <param name="id">voucher type id</param>
        /// <returns></returns>
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
