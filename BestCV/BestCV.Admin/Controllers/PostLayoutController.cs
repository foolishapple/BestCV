
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("post-layout")]
    [Authorize]
    public class PostLayoutController : BaseController
    {
        private readonly ILogger<PostLayoutController> _logger;

        public PostLayoutController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PostLayoutController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
