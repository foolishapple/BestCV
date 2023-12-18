using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("salary-range")]
    [Authorize]
    public class SalaryRangeController : BaseController
    {
        private readonly ILogger<SalaryRangeController> _logger;
        public SalaryRangeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SalaryRangeController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
