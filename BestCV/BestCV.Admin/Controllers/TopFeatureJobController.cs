using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("top-feature-job")]
    [Authorize]
    public class TopFeatureJobController : BaseController
    {
        private readonly ILogger<TopFeatureJobController> _logger;
        public TopFeatureJobController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TopFeatureJobController>();
        }

        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
