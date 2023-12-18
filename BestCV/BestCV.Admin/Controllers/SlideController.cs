using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("slide")]
    [Authorize]
    public class SlideController : BaseController
    {
        private readonly ILogger<SlideController> _logger;

        public SlideController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SlideController>();
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
