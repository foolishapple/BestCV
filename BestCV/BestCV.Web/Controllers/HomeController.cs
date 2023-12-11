using Microsoft.AspNetCore.Mvc;
using BestCV.Web.Models;
using System.Diagnostics;
using BestCV.Application.Models.Candidates;
using BestCV.Core.Utilities;
using BestCV.Core.Entities;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BestCV.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using BestCV.Domain.Aggregates.Company;
using System.Text;

namespace BestCV.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICompanyService service;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICandidateNotificationService _candidateNotificationService;
        private readonly ICandidateHomeService _candidateHomeService;
        private readonly IJobService _jobService;
        public HomeController(ILoggerFactory loggerFactory, ICompanyService _service, IConfiguration configuration, ICandidateHomeService candidateHomeService, ICandidateNotificationService candidateNotificationService, IJobService jobService)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _configuration = configuration;
            _candidateNotificationService = candidateNotificationService;
            service = _service;
            _candidateHomeService = candidateHomeService;
            _jobService = jobService;
        }

        public IActionResult Index()
        {
            var countJob =  _candidateHomeService.CountJob();
            ViewBag.CountJob = countJob;
            return View();
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

        //[Route("dang-ky")]
        //public IActionResult Signup()
        //{
            
        //    return View();
        //}

        [Route("tim-kiem-viec-lam")]
        public IActionResult AllSearchByCategory(string tukhoa, int? diadiem, int? danhmuc)
        {
            ViewBag.Keyword = tukhoa != null ? tukhoa : "";
            ViewBag.Location = diadiem != null ? diadiem : 0;
            ViewBag.Category = danhmuc != null ? danhmuc : 0;
            return View();
        }

        [Route("thong-tin-cong-ty/{slug}")]
        public async Task<IActionResult> FindBestCompanies([Required] string slug)
        {
            try
            {
                int id = 0;
                Int32.TryParse(slug.Split("-").Last(), out id);
                ViewBag.id = id;
                if(await _candidateHomeService.CompanyIsActive(id))
                {
                    return View("FindBestCompanies");
                }
                return StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get company detail page");
                return StatusCode(400);
            }
        }

        [Route("thong-tin-cong-ty")]
        public IActionResult FindBestCompanies()
        {
            return View();
        }

        [Route("tim-kiem-cong-ty")]
        public IActionResult FindAllCompanies(string keyword, int? location, int? category)
        {
            ViewBag.Keyword = keyword != null ? keyword : "";
            ViewBag.Location = location != null ? location : 0;
            ViewBag.Category = category != null ? category : 0;
            return View();
        }


        [Route("top-career-advice")]
        public IActionResult TopCareerAdvice()
        {
            return View();
        }

        [Route("job-category-search")]
        public IActionResult SearchByCategory()
        {
            return View();
        }


        [Route("chi-tiet-cong-viec/{slug}")]
        public async Task<IActionResult> ApplyJob(string slug)
        {
            try
            {
                var arrSlug = slug.Split('-');
                long jobId = 0;
                Int64.TryParse(arrSlug[arrSlug.Length - 1], out jobId);
                ViewBag.DetailJobId = jobId;
                try
                {
                    await _jobService.AddViewCount(Convert.ToInt64(jobId));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to add view count with jobId" + jobId);
                }
                if (await _candidateHomeService.JobIsActive(jobId) )
                {
                    return View();
                }
                return StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get job detail page");
                return StatusCode(400);
            }
            
        }

        //[HttpGet("xac-thuc-tai-khoan/{code}/{hash}")]
        //public async Task<IActionResult> Verified(string code, string hash)
        //{
        //    try
        //    {
        //        var objectEmail = new ConfirmEmailCandidateDTO()
        //        {
        //            Value = code,
        //            Hash = hash
        //        };

        //        var verifyCodeConfirmEmailAPi = _configuration["VerifyCodeConfirmEmailApi"];
 

        //        using var httpClient = new HttpClient();
        //        var result = await httpClient.SendRequestAsync<ConfirmEmailCandidateDTO, DionResponse>(verifyCodeConfirmEmailAPi, objectEmail);

        //        if (result.IsSucceeded)
        //        {
        //            ViewBag.Expired = false;
        //            return View("Verified");
        //        }
        //        else if (!result.IsSucceeded && result.Status == 400)
        //        {
        //            ViewBag.Expired = true;
        //            return View("Verified");
        //        }
        //        else
        //        {
        //            return View("PageNotFound");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex,"User verify email doesn't success");
        //        return View("BadRequest");
        //    }
        //}


        [HttpGet("xac-thuc")]
        public IActionResult Verified()
        {
            return View();
        }

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

        [HttpGet("loi")]
        public IActionResult BadRequest()
        {
            return View();
        }
        [Route("cap-nhat-mat-khau")]
        public IActionResult UpdatePasswordCandidate() 
        {
            //ViewBag.Route = Request.Path.Value;
            return View();
        }
        [Route("cai-dat-thong-bao")]
        public IActionResult UpdateNotiEmailCandidate()
        {
            return View();
        }


        [Route("dang-nhap")]
        public IActionResult SignIn(string code)
        {
            ViewBag.SignIn = true;
            return View();
        }

        [Route("cai-dat-goi-y-viec-lam")]
        public IActionResult JobSuggestionSetting()
        {
            //ViewBag.Route = Request.Path.Value;
            return View();
        }

        [Route("cai-dat-thong-tin-ca-nhan")]
        public IActionResult UpdateProfileCandidate()
        {
            //ViewBag.Route = Request.Path.Value;
            return View();
        }

        [Route("call-back")]
        public IActionResult CallBack(string code)
        {
            return View();
        }

        [Route("thong-bao")]

        public IActionResult Notification()
        {
            
             
                return View();
            
           
        }
        [Route("chi-tiet-thong-bao/{encodedId}")]
        public async Task<IActionResult> NotificationDetail(string encodedId)
        {
            try
            {
                long id = Convert.ToInt64(Encoding.UTF8.GetString(Convert.FromBase64String(encodedId)));

                var data = await _candidateNotificationService.GetByIdAsync(id);
                var candidateNotification = data?.Resources as CandidateNotification;

                if (candidateNotification == null)
                {
                    return View("PageNotFound");
                }
                else
                {
                    return View("NotificationDetail", candidateNotification);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có vấn đề trong quá trình giải mã
                return View("PageNotFound");
            }
        }


        [Route("list-company")]
        public async Task<IActionResult> ListTopCompany()
        {
            var data = await _candidateHomeService.GetListTopCompanyShowonHomepage();
            return PartialView("_ListTopCompany", data.Resources);
        }

        [Route("our-service")]
        public async Task<IActionResult> OurService()
        {
            return PartialView("_OurService");
        }

        [Route("coaching")]
        public async Task<IActionResult> Coaching()
        {
            return PartialView("_Coaching");
        }

        [Route("top-job-on-home-page")]
        public async Task<IActionResult> ListTopJob()
        {
            //var data = await _candidateHomeService.GetListTopJobShowonHomepage();
            return PartialView("_ListTopJob");
        }

        [Route("list-job-category")]
        public async Task<IActionResult> ListJobCategory()
        {
          var data = await _candidateHomeService.GetListJobCategoryShowonHomepage();
            return PartialView("_ListJobCategory", data.Resources);
        }

        [Route("list-post")]
        public async Task<IActionResult> ListPost()
        {
            var data = await _candidateHomeService.GetListTopPostShowonHomepage();
            return PartialView("_ListPost", data.Resources);
        }



        [Route("lich-phong-van")]
        public IActionResult InterviewSchedule()
        {
            //ViewBag.Route = Request.Path.Value;
            return View();
        }


        [Route("lien-he")]        
        public IActionResult Contact()
        {
            return View();
        }

        [Route("viec-lam-moi")]
        public IActionResult TopFeatureJob()
        {
            return View();
        }

        [Route("viec-lam-tot-nhat")]
        public IActionResult TopExtraJob()
        {
            return View();
        }
        [Route("viec-lam-gap")]
        public IActionResult TopUrgentJob()
        {
            return View();
        }

        [Route("viec-lam-quan-ly")]
        public IActionResult TopManagementJob()
        {
            return View();
        }

        [Route("tong-quan")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Route("viec-lam-phu-hop")]
        public IActionResult TopSuitableJob()
        {
            return View();
        }
        [Route("quan-ly-cong-viec")]
        public IActionResult ManageJob()
        {
            //ViewBag.Route = Request.Path.Value;
            return View();
        }

        [Route("cong-ty-da-theo-doi")]
        public IActionResult GetListCandidateFollowCompany()
        {
            //ViewBag.Route = Request.Path.Value;
            return View();
        }
    }
}