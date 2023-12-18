using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("service-package-group")]
    [Authorize]

    public class ServicePackageGroupController : BaseController
    {
        [Route("list")]

        public IActionResult AdminList()
        {
            return View();
        }
    }
}
