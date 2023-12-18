using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("coupon-type")]
    [Authorize]
    public class CouponTypeController : BaseController
    {
        private readonly ILogger<CouponTypeController> _logger;
        public CouponTypeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CouponTypeController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
