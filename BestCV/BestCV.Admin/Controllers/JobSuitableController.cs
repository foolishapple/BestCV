using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("job-suitable")]
    [Authorize]
    public class JobSuitableController : BaseController
    {
        private readonly ILogger<JobSuitableController> _logger;
        public JobSuitableController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JobSuitableController>();

        }

        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
