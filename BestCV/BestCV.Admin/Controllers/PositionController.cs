using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("position")]
    [Authorize]
    public class PositionController : BaseController
    {
        private readonly ILogger<PositionController> _logger;

        public PositionController(ILogger<PositionController> logger)
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
