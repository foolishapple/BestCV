using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("skill")]
    [Authorize]
    public class SkillController : BaseController
    {
        private readonly ILogger<SkillController> _logger;
        public SkillController(ILogger<SkillController> logger)
        {
            _logger = logger;
        }


        [HttpGet("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
