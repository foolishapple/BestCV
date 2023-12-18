using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("job-position")]
    [Authorize]
    public class JobPositionController : BaseController
    {
        private readonly ILogger<JobPositionController> _logger;
        public JobPositionController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JobPositionController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
