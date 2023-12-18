using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("order-status")]
    [Authorize]
    public class OrderStatusController : BaseController
    {
        private readonly ILogger<OrderStatusController> _logger;

        public OrderStatusController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OrderStatusController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
