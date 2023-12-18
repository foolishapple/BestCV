using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("field-of-activity")]
    [Authorize]
    public class FieldOfActivityController : BaseController
    {
        private readonly ILogger<FieldOfActivityController> _logger;

        public FieldOfActivityController(ILogger<FieldOfActivityController> logger)
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
