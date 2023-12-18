using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("tag")]
    [Authorize]
    public class TagController : BaseController
    {
        private readonly ILogger<TagController> _logger;
        private readonly ITagService tagService;
        public TagController(ILogger<TagController> logger, ITagService tagService)
        {
            _logger = logger;
            this.tagService = tagService;
        }

        [HttpGet("list")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
