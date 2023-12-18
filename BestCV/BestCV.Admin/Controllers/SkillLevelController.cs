using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("skill-level")]
    [Authorize]
    public class SkillLevelController : BaseController
    {
        private readonly ILogger<SkillLevelController> _logger;
        public SkillLevelController(ILogger<SkillLevelController> logger)
        {
            _logger = logger;
        }

        [HttpGet("list")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
