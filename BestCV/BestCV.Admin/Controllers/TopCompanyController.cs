using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("top-company")]
    [Authorize]
    public class TopCompanyController : BaseController
    {
        private readonly ILogger<TopCompanyController> _logger;
        public TopCompanyController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TopCompanyController>();
        }

        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
