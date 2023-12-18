using Microsoft.AspNetCore.Mvc;

namespace BestCV.ScheduledJob.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
