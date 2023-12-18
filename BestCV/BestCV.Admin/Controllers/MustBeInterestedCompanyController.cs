using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("must-be-interesterd-company")]
    [Authorize]

    public class MustBeInterestedCompanyController : BaseController
    {
        private readonly ILogger<MustBeInterestedCompanyController> _logger;
        public MustBeInterestedCompanyController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MustBeInterestedCompanyController>();
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
