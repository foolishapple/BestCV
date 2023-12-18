using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("coupon")]
    [Authorize]
    public class CouponController : BaseController
    {
        private readonly ILogger<CouponController> _logger;
        public CouponController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CouponController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
