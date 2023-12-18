using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("top-job-extra")]
    [Authorize]
    public class TopJobExtraController : BaseController
    {
        private readonly ILogger<TopJobExtraController> logger;

        public TopJobExtraController(ILogger<TopJobExtraController> _logger)
        {
            logger = _logger;
        }

        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
