using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("job-status")]
    [Authorize]
    public class JobStatusController : BaseController
    {
        private readonly ILogger<JobStatusController> _logger;

        public JobStatusController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JobStatusController>();
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
