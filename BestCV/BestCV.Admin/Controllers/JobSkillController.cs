using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("job-skill")]
    [Authorize]
    public class JobSkillController : BaseController
	{
		private readonly ILogger<JobSkillController> _logger;
		public JobSkillController(ILoggerFactory loggerFactory)
		{
            _logger = loggerFactory.CreateLogger<JobSkillController>();

        }
        [Route("list")]
		public IActionResult AdminList()
		{
			return View();
		}
	}
}
