using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("job-type")]
    [Authorize]
    public class JobTypeController : BaseController
    {
        private readonly ILogger<JobTypeController> _logger;

        public JobTypeController(ILogger<JobTypeController> logger)
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
