using BestCV.API.Utilities;
using BestCV.Application.Models.EmployerOrder;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.EmployerOrder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployerOrderController : BaseController
    {
        private readonly IEmployerOrderService service;
        private readonly ILogger<EmployerOrderController> logger;

        public EmployerOrderController(IEmployerOrderService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<EmployerOrderController>();
        }


        [HttpPost("list-employer-order-aggregates")]
        public async Task<IActionResult> ListOrderAggregates(DTParameters parameters)
        {
            try
            {
                return Json(await service.ListOrderAggregates(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load list order aggregates");
                return BadRequest();
            }
        }

  
		[HttpGet("list-order-status-selected")]
        public async Task<IActionResult> ListOrderStatusSelected()
        {
            try
            {
                var data = await service.ListOrderStatusSelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected order status");
                return BadRequest();
            }
        }
        
 
		[HttpGet("list-payment-method-selected")]
        public async Task<IActionResult> ListPaymentMethodSelected()
        {
            try
            {
                var data = await service.ListPaymentMethodSelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected payment method");
                return BadRequest();
            }
        }

 
        [HttpPut("quick-isapproved")]
        public async Task<IActionResult> QuickIsApproved([Required] long id)
        {
            try
            {
                var response = await service.QuickIsApprovedAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to quick isapproved async");
                return BadRequest();
            }
        }

 
        [HttpGet("detail-admin/{id}")]
        public async Task<IActionResult> DetailAdmin([Required] long id)
        {
            try
            {
                var response = await service.AdminDetailAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get detail order by id: {id}");
                return BadRequest();
            }
        }

  
        [HttpGet]
        [Route("list-order-detail/{id}")]
        public async Task<IActionResult> ListOrderDetailByOrderId(long id)
        {
            try
            {
                var response = BestCVResponse.Success(await service.ListOrderDetailByOrderId(id));
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }


        [HttpPut]
        [Route("update-info-order")]
        public async Task<IActionResult> UpdateInfoOrder([FromBody] UpdateInfoOrderDTO model)
        {
            try
            {
                var info = await service.UpdateInfoOrder(model);
                return Ok(info);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update info order");
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("add-order")]
        public async Task<IActionResult> AddOrder([FromBody] CreateEmployerOrderDTO model)
        {
            try
            {
                var employerId = this.GetLoggedInUserId();
                model.EmployerId = employerId;
                var data = await service.AddOrder(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update info order");
                return BadRequest();
            }
        }


        [HttpPost("list-by-employer")]
        public async Task<IActionResult> ListByEmployer(DTPagingEmployerOrderParameters parameters)
        {
            try
            {
                var employerId = this.GetLoggedInUserId();
                parameters.EmployerId = employerId;
                return Json(await service.PagingByEmployerId(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load list order aggregates");
                return BadRequest();
            }
        }
        [HttpGet("detail-by-order-id/{orderId}")]
        public async Task<IActionResult> DetailByOrderId(long orderId)
        {
            try
            {
                var data = await service.DetailByOrderId(orderId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load detail by order id");
                return BadRequest();
            }
        }

        [HttpPut("cancel-order/{orderId}")]
        public async Task<IActionResult> CancelOrder(long orderId)
        {
            try
            {
                var data = await service.CancelOrder(orderId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to cancel order by order id");
                return BadRequest();
            }
        }
        [HttpPut("update-order-status")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateEmployerOrderStatusDTO obj)
        {
            try
            {
                var result = await service.UpdateOrderStatus(obj);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to update order status");
                return BadRequest();
            }
        }
    }
}
