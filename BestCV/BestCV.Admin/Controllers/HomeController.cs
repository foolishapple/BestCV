using BestCV.Admin.Models;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace BestCV.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminAccountService _service;

        public HomeController(ILogger<HomeController> logger,IAdminAccountService service)
        {
            _logger = logger;
            _service = service;
        }
        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("Profile");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("navigation")]
        public IActionResult Navigation()
        {
            return PartialView("_AdminNavigationLayout");
        }
		public IActionResult Error500()
		{
			return View();
		}
		public IActionResult Error404()
		{
			return View();
		}
        [Route("sign-in")]
        public IActionResult Login(string redirectURL)
        {
            HttpContext.Response.Cookies.Delete("Authorization");
            ViewBag.redirectURL = redirectURL;
            return View("_Login");
        }
        [Route("sign-out")]
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 401)
            {
                return RedirectToAction("Login");
            }
            return View("Error500");
        }
        [Route("profile")]
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("forgot-password")]
        public IActionResult ForgotPasswordAdmin()
        {
            return View();
        }

        [HttpGet("doi-mat-khau/{code}/{hash}")]
        public async Task<IActionResult> ResetPasswordAdmin(string code, string hash)
        {
            try
            {
                var tokenValid = await _service.CheckKeyValid(code, hash);
				ViewBag.tokenValid = tokenValid;
				ViewBag.Value = code;
                ViewBag.Hash = hash;
                if (tokenValid)
                {
                    return View("~/Views/Home/ResetPasswordAdmin.cshtml");
                }
                else
                {
                    return View("~/Views/Shared/Error404.cshtml");
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when return view page Error404");
                return View("~/Views/Shared/Error404.cshtml");
            }
        }

    }
}