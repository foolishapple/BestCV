
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("post-type")]
    [Authorize]
    public class PostTypeController : BaseController
    {
        private readonly ILogger<PostTypeController> _logger;

        public PostTypeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PostTypeController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
