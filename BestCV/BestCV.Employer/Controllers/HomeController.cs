using BestCV.Application.Models.Employer;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Employer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace BestCV.Employer.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployerNotificationService _employerNotificationService;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,IEmployerNotificationService employerNotificationService)
        {
            _logger = logger;
            _employerNotificationService = employerNotificationService;
        }

        public IActionResult Index()
        {
            ViewBag.CurentPage = "company-dashboard";
            return View("Dashboard");
        }

        // Controller Demo
        [Route("tong-quan")]
        public IActionResult CompanyDashboard()
        {
            ViewBag.CurentPage = "company-dashboard";
            return View("Dashboard");
        }


        [Route("dang-tin-tuyen-dung")]
        public IActionResult NewJobOffer(long? recruitmentCampaginId)
        {
            ViewBag.Route = Request.Path.Value;
            ViewBag.CurentPage = "new-job-offer";
            if (recruitmentCampaginId == null)
            {
                return StatusCode(400);
            }
            ViewBag.recruitmentCampaginId = recruitmentCampaginId;
            return View("NewJobOffer");
        }

        [Route("quan-ly-tin-tuyen-dung")]
        public IActionResult ManageJobs()
        {
            ViewBag.Route = Request.Path.Value;
            ViewBag.CurentPage = "manage-jobs";
            return View("ManageJobs");
        }

        [Route("quan-ly-ung-vien")]
        public IActionResult Candidates()
        {
            ViewBag.Route = Request.Path.Value;
            ViewBag.CurentPage = "candidates";
            return View("Candidates");
        }

        [Route("dich-vu-cua-toi")]
        public IActionResult Subscriptions()
        {
            ViewBag.Route = Request.Path.Value;
            ViewBag.CurentPage = "subscriptions";
            return View("Subscriptions");
        }

        [Route("hom-thu")]
        public IActionResult Inbox()
        {
            ViewBag.CurentPage = "inbox";
            return View("Inbox");  
        }

        [Route("thong-bao")]
        public IActionResult Notifications()
        {
            ViewBag.CurentPage = "notifications";
            return View("Notification");
        }

        [Route("chien-dich-tuyen-dung")]
        public  IActionResult RecruitmentCampaigns()
        {
            ViewBag.Route = Request.Path.Value;
            ViewBag.CurentPage = "recruitment-campaigns";
            return View();
        }

        // Controller Demo end

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region Trang hệ thống
        [HttpGet("khong-tim-thay-trang")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [HttpGet("chua-dang-nhap")]
        public IActionResult UnAuthorized()
        {
            return View();
        }


        [HttpGet("khong-co-quyen")]
        public IActionResult Forbidden()
        {
            return View();
        }

        [HttpGet("loi")]
        public IActionResult BadRequested()
        {
            return View();
        }
        #endregion

        [HttpGet("tin-tuyen-dung/{id}")]
        public IActionResult JobDetail(long id)
        {
            ViewBag.Id = id;
            return View();
        }
        [Route("chi-tiet-thong-bao/{encodedId}")]
        public async Task<IActionResult> NotificationDetail(string encodedId)
        {
            try
            {
                long id = Convert.ToInt64(Encoding.UTF8.GetString(Convert.FromBase64String(encodedId)));
                var data = await _employerNotificationService.GetByIdAsync(id);
                var employerNotification = data?.Resources as EmployerNotification ;
                if (employerNotification == null)
                {
                    return View("PageNotFound");
                }
                else
                {
                    return View("NotificationDetail", employerNotification);
                }
            }
            catch (Exception ex)
            {
                return View("PageNotFound");
            }
        }

        [Route("cv-ung-vien/{id}")]
        public IActionResult CandidateCV()
        {
            return View();
        }

        [Route("lich-phong-van")]
        public IActionResult InterViewScheduleEmployer()
        {
            ViewBag.Route = Request.Path.Value;
            return View();
        }

        [Route("tim-kiem-ung-vien")]
        public IActionResult SearchCandidate()
        {
            ViewBag.Route = Request.Path.Value;
            return View("SearchCandidate");
        }

        [Route("chi-tiet-cv-ung-vien/{candidateId}")]
        public IActionResult DetailCVFindCandidate(long candidateId)
        {
            ViewBag.CandidateCVID = candidateId;
            return View();
        }
    }
}