using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("order-type")]
    [Authorize]
    public class OrderTypeController : BaseController
    {
        private readonly ILogger<OrderTypeController> _logger;

        public OrderTypeController(ILogger<OrderTypeController> logger)
        {
            _logger = logger;
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
