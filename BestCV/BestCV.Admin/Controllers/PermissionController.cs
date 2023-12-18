using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("permission")]
    [Authorize]
    public class PermissionController : BaseController
    {
        [Route("list")]
        public IActionResult List()
        {
            return View();
        }
    }
}
