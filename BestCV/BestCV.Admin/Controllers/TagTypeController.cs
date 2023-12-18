using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("tag-type")]
    [Authorize]

    public class TagtypeController : BaseController
    {
        private readonly ILogger<TagtypeController> _logger;

        public TagtypeController(ILogger<TagtypeController> logger)
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
