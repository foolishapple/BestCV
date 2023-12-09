using BestCV.Application.Models.Employer;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Employer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BestCV.Employer.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmployerService service;

        public AccountController(ILoggerFactory loggerFactory, IConfiguration configuration, IEmployerService employerService)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _configuration = configuration;
            service = employerService;
        }

        [Route("dang-nhap")]
        public IActionResult SignIn()
        {
            return View("SignIn");
        }

        [Route("cap-nhat-mat-khau")]
        public IActionResult ChangePassword()
        {
            ViewBag.Route = Request.Path.Value;
            return View("ChangePassword");
        }

        [Route("cap-nhat-thong-tin-ca-nhan")]
        public IActionResult EditProfile()
        {
            ViewBag.Route = Request.Path.Value;
            return View();
        }

        [Route("dang-ky")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet("xac-thuc-tai-khoan/{code}/{hash}")]
        public IActionResult Verify(string code, string hash)
        {
            try
            {
                ViewBag.Value = code;
                ViewBag.Hash = hash;
                return View("~/Views/Home/Verify.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when return view page verify");
                return View("~/Views/Home/BadRequest");
            }
        }

        [Route("cap-nhat-thong-tin-cong-ty")]
        public IActionResult EditProfileCompany()
        {
            ViewBag.Route = Request.Path.Value;
            return View();
        }
        [HttpGet("doi-mat-khau/{code}/{hash}")]
        public async Task<IActionResult>  ResetPassword(string code, string hash)
        {
            try
            {
                var tokenValid = await service.CheckKeyValid(code, hash);
                ViewBag.Value = code;
                ViewBag.Hash = hash;
                if(tokenValid)
                {
                    return View("~/Views/Account/ResetPassword.cshtml");
                }
                else
                {
                    return View("~/Views/Home/BadRequest.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when return view page verify");
                return View("~/Views/Home/BadRequest.cshtml");
            }
        }

        [Route("mua-dich-vu")]
        public IActionResult BuyService()
        {
            return View();
        }

        [Route("gio-hang")]
        public IActionResult MyCart(int? dv)
        {
            ViewBag.EmployerServicePackageId = dv != null ? dv : 0;
            return View();
        }

        [Route("don-hang-cua-toi")]
        public IActionResult MyOrder()
        {
            return View();
        }
    }
}