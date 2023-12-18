using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("role")]
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
        }
        [Route("list")]
        public IActionResult List()
        {
            return View();
        }
    }
}
