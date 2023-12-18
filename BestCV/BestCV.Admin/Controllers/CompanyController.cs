using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("company")]
    [Authorize]
    public class CompanyController : BaseController
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CompanyController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
