using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("multimedia-type")]
    [Authorize]
    public class MultimediaTypeController : BaseController
    {
        private readonly ILogger<MultimediaTypeController> _logger;

        public MultimediaTypeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MultimediaTypeController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
