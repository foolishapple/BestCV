using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("role-permission")]
    [Authorize]
    public class RolePermissionController : BaseController
    {
        [Route("list")]
        public IActionResult List()
        {
            return View();
        }
    }
}
