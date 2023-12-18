using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("experience-range")]
    [Authorize]
    public class ExperienceRangeController : BaseController
    {
        private readonly ILogger<ExperienceRangeController> _logger;
        public ExperienceRangeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExperienceRangeController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
