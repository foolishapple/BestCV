using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("job-reference")]
    [Authorize]
    public class JobReferenceController : BaseController
    {
        private readonly ILogger<JobReferenceController> _logger;
        public JobReferenceController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JobReferenceController>();

        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
