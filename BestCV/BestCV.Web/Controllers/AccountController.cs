using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BestCV.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> logger;
        private readonly IConfiguration configuration;
        private readonly ICandidateService service;

        public AccountController(ILoggerFactory loggerFactory, IConfiguration _configuration, ICandidateService _service)
        {
            logger = loggerFactory.CreateLogger<AccountController>();
            configuration = _configuration;
            service = _service;
        }
        public IActionResult Index()
        {
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
                logger.LogError(ex, "Error when return view page verify");
                return View("~/Views/Home/BadRequest");
            }
        }
        [HttpGet("doi-mat-khau/{code}/{hash}")]
        public async Task<IActionResult> ResetPassword(string code, string hash)
        {
            try
            {
                var tokenValid = await service.CheckKeyValid(code, hash);
                ViewBag.Value = code;
                ViewBag.Hash = hash;
                if (tokenValid)
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
                logger.LogError(ex, "Error when return view page verify");
                return View("~/Views/Home/BadRequest.cshtml");
            }
        }

        [Route("thong-tin-ca-nhan/{id}")]
        public async Task<IActionResult> DetailCandidate(long id)
        {
            try
            {
                ViewBag.candidateId = id;
                if(await service.IsActive(id))
                {
                    return View("DetailCandidate");
                }
                return StatusCode(404);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get candidate detail page");
                return StatusCode(400);
            }
        }


        public IActionResult GetListSaveJob()
        {
            return View();
        }


        public IActionResult GetListViewedJob()
        {
            return View();
        }

        
        
    }
}
