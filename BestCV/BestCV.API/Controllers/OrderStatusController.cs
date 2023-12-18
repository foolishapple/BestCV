using BestCV.Application.Models.JobCategory;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/order-status")]
    [ApiController]
    public class OrderStatusController : BaseController
    {
        private readonly IOrderStatusService orderStatusService;
        private readonly ILogger<OrderStatusController> logger;
        public OrderStatusController(IOrderStatusService orderStatusService, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<OrderStatusController>();
            this.orderStatusService = orderStatusService;
        }


        #region CRUD
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: list order status
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await orderStatusService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get order status");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: detail order status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await orderStatusService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get order status by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: insert order status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertOrderStatusDTO obj)
        {
            try
            {
                var response = await orderStatusService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert order status: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: update order status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOrderStatusDTO obj)
        {
            try
            {

                var response = await orderStatusService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update order status: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: delete order status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await orderStatusService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete order status by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
