using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("job-category")]
    [Authorize]
    public class JobCategoryController : BaseController
    {
        private readonly ILogger<JobCategoryController> _logger;

        public JobCategoryController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JobCategoryController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
