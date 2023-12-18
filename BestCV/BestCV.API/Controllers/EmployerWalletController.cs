using BestCV.API.Utilities;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
    [Route("api/employer-wallet")]
    [ApiController]
    public class EmployerWalletController : BaseController
    {
        private readonly IEmployerWalletService _employerWalletService;
        private readonly ILogger<EmployerWalletController> _logger;
        public EmployerWalletController(IEmployerWalletService employerWalletService, ILoggerFactory logger)
        {
            _employerWalletService = employerWalletService;
            _logger = logger.CreateLogger<EmployerWalletController>();
        }

        [HttpGet]
        [Route("detail-credit-wallet")]
        public async Task<IActionResult> GetDetailCreditByEmployerId()
        {
            try
            {
                var employerId = this.GetLoggedInUserId();
                var data = await _employerWalletService.GetCreditWalletByEmployerId(employerId);
                return Ok(data);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed to get detail credit");
                return BadRequest();
            }
        }
    }
}
