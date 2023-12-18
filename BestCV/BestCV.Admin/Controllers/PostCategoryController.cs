using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("post-category")]
    [Authorize]
    public class PostCategoryController : BaseController
    {
        private readonly ILogger<PostCategoryController> _logger;

        public PostCategoryController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PostCategoryController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
