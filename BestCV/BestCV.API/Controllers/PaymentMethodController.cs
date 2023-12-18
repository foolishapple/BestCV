using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Models.PaymentMethod;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/payment-method")]
    [ApiController]
    public class PaymentMethodController : BaseController
    {
        private readonly IPaymentMethodService paymentMethodService;
        private readonly ILogger<PaymentMethodController> logger;
        public PaymentMethodController(IPaymentMethodService paymentMethodService, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<PaymentMethodController>();
            this.paymentMethodService = paymentMethodService;
        }

        #region CRUD
  
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await paymentMethodService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get payment method");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await paymentMethodService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get payment method by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPaymentMethodDTO obj)
        {
            try
            {
                var response = await paymentMethodService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert payment method: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentMethodDTO obj)
        {
            try
            {

                var response = await paymentMethodService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update payment method: {obj}");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await paymentMethodService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete payment method by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
