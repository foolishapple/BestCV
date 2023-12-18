using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("job")]
    [Authorize]
    public class JobController : BaseController
    {
        private readonly ILogger<JobController> _logger;

        public JobController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JobController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
