using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("service-package-type")]
    [Authorize]

    public class ServicePackageTypeController : BaseController
    {
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}
