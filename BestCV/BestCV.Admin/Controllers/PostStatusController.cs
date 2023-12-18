using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("post-status")]
    [Authorize]
    public class PostStatusController : BaseController
    {
        private readonly ILogger<PostStatusController> _logger;

        public PostStatusController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PostStatusController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
