using BestCV.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using RotativaIO.NetCore;
using System.Text.Json;

namespace BestCV.Web.Controllers
{
    public class CVController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CVController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


   
        [Route("viet-cv")]
        [Route("viet-cv/{candidateCVId}")]
        public IActionResult CvBuilder(long? candidateCVId)
        {
            ViewBag.Route = Request.Path.Value;
            ViewBag.CandidateCVId = candidateCVId != null ? candidateCVId : 0;
            return View();
        }


        [Route("quan-ly-cv")]
        public IActionResult CVManager()
        {
            ViewBag.Route = Request.Path.Value;
            return View();
        }


        [Route("quan-ly-cv-pdf")]
        public IActionResult CVPDFManager()
        {
            ViewBag.Route = Request.Path.Value;
            return View();
        }

  
        [Route("demo-view-as-pdf")]
        public IActionResult DemoViewAsPDF()
        {
            return new ViewAsPdf("DemoViewAsPDF");
        }

     
        [HttpPost("print-preview")]
        public IActionResult PrintPreview([FromBody] SavePDFCandidateCVDTO candidateCVDTO)
        {
            return View("SaveCandidateCVPDF", candidateCVDTO);
        }


        [Route("print-to-pdf")]
        public IActionResult PrintToPDF()
        {
            //ViewAsPdf viewAsPdf = new ViewAsPdf("PrintToPDF",candidateCVDTO);
            //byte[] pdfData = await viewAsPdf.BuildFile(ControllerContext);
            //string fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "pdf", "PrintToPDFTest.pdf");
            //using (var fileStream = new FileStream(fullPath.Replace("Jobi.Web", "Jobi.Storage"), FileMode.Create, FileAccess.Write))
            //{
            //    fileStream.Write(pdfData, 0, pdfData.Length);
            //}
            //var newItem = new SavePDFCandidateCVDTO()
            //{
            //    AdditionalCSS = "#cv-item-avatar img {    width: 100%;    border-radius: 10px;}span[contenteditable=true]:focus-visible{    outline: 1px dashed var(--cv-primary-color);    outline-offset: -2px;}.cv-col-4{    color: white;    line-height: 2;    background-color: #626262;}.cv-col-8 {    line-height: 2;}.cv-col-8 .cv-item-header {    margin-top: 4px;    border-width: 2px;    border-style: solid;    border-radius: 10px;    padding: 6px;    border-color: rgb(255 177 86 / 100%);}#cv-item-contact-info .cv-item-body {    }#cv-item-contact-info .cv-item-body i {    font-size: 13px;     width: 20px;}",
            //    Content = "<div class=\"row\">            <div class=\"col-4\" onmouseenter=\"onMouseEnterColumn(this)\" onmouseleave=\"onMouseLeaveColumn(this)\">                <div class=\"cv-item cv-item-avatar movable padding\">                    <div class=\"cv-item-body\" style=\"border: 2px solid #FFB156;\">                        <img src=\"https://localhost:7049/uploads/candidates/avatars/20230825154837640_unnamed.jpg\" alt=\"\">                    </div>                </div>                <div class=\"cv-item cv-item-contact-info movable padding\">                    <div class=\"cv-item-header\">                        <div class=\"cv-item-title\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"color: white\">                            <b>THÔNG TIN CÁ NHÂN</b>                        </div>                    </div>                    <div class=\"cv-item-body padding\">                        <div class=\"cv-item-email\">                            <i class=\"fa fa-envelope\"></i>                            <span contenteditable=\"true\" onclick=\"onClickElement(this)\">nguyenducthanh15698@gmail.com</span>                        </div>                        <div class=\"cv-item-phone\">                            <i class=\"fa fa-circle-phone\"></i>                            <span contenteditable=\"true\" onclick=\"onClickElement(this)\">0353175663</span>                        </div>                        <div class=\"cv-item-website\">                            <i class=\"fa fa-globe\"></i>                            <span contenteditable=\"true\" onclick=\"onClickElement(this)\">facebook.com/thao.dinh</span>                        </div>                        <div class=\"cv-item-address\">                            <i class=\"fa fa-location-dot\"></i>                            <span contenteditable=\"true\" onclick=\"onClickElement(this)\">766 Đê la thành </span>                        </div>                    </div>                </div>                <div id=\"cv-item-skill\" class=\"cv-item movable padding\">                    <div class=\"cv-item-header\">                        <div class=\"cv-item-title\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"color: white\">                            <b>KỸ NĂNG</b>                        </div>                    </div>                    <div class=\"cv-item-body padding\">                        <div class=\"cv-item-content\" contenteditable=\"true\" onclick=\"onClickElement(this)\">                            Tiếng Anh                        </div>                        <div class=\"cv-item-content\" contenteditable=\"true\" onclick=\"onClickElement(this)\">                            Phân tích nhu cầu người dùng                        </div>                        <div class=\"cv-item-content\" contenteditable=\"true\" onclick=\"onClickElement(this)\">                            Sử dụng Pivotal Tracker                        </div>                        <div class=\"cv-item-content\" contenteditable=\"true\" onclick=\"onClickElement(this)\">                            Vẽ Wireframe                        </div>                    </div>                </div>                <div id=\"cv-item-skill\" class=\"cv-item movable padding\">                    <div class=\"cv-item-header\">                        <div class=\"cv-item-title\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"color: white\">                            <b>CHỨNG CHỈ</b>                        </div>                    </div>                    <div class=\"cv-item-body movable copyable\">                        <div style=\"display: flex; justify-content: space-between;\">                            <span class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; font-size: 14px;\">                                <b>GOOGLE ADWORDS</b>                            </span>                            <span class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; font-size: 14px;\">11/2016                            </span>                        </div>                        <div class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"margin-top: -20px; font-size: 14px; line-height: 25px;\">                            Đọc và thi 2 chứng chỉ trong 14 ngày                            <ul>                                <li>AdWords căn bản</li>                                <li>Quảng cáo tìm kiếm</li>                            </ul>                        </div>                    </div>                    <div class=\"cv-item-body movable copyable\">                        <div style=\"display: flex; justify-content: space-between;\">                            <span class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; font-size: 14px;\">                                <b>TOEIC</b>                            </span>                            <span class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; font-size: 14px;\">12/2012                            </span>                        </div>                        <div class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"margin-top: -20px; font-size: 14px; line-height: 25px;\">                            750 điểm. Có thể:                            <ul>                                <li>Đọc và viết tài liệu tham khảo</li>                                <li>Viết business và support email</li>                                <li>Nghe, nói và take note khi thảo luận công việc qua các buổi họp, call với                                    khách hàng</li>                            </ul>                        </div>                    </div>                </div>            <div class=\"d-none\" id=\"cv-btn-add-item\" onclick=\"addItem(this)\" style=\"top: 1187.33px;\">    <div class=\"btn\">        <i class=\"fa-solid fa-plus\"></i>    </div>    \\x3C!-- <div>Thêm mục</div> --></div></div>            <div class=\"col-8 padding\" onmouseenter=\"onMouseEnterColumn(this)\" onmouseleave=\"onMouseLeaveColumn(this)\">                <div id=\"cv-item-name-card\" class=\"cv-item movable padding\" style=\"margin-top: -10px;\">                    <div class=\"cv-item-body\">                        <div class=\"cv-item-fullname\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"font-size: 24px; text-transform: uppercase;\"><b>Thanh Nguyễn</b></div>                        <div class=\"cv-item-nominee\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"font-size: 16px; letter-spacing: 2px;\"><b>Developer</b></div>                    </div>                </div>                <div class=\"cv-item cv-item-career-goals movable padding\">                    <div class=\"cv-item-header\">                        <div class=\"cv-item-title cv-focus\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"font-size: 16px; line-height: 32px;\">                            <b>MỤC TIÊU NGHỀ NGHIỆP</b>                        </div>                    </div>                    <div class=\"cv-item-body\">                        <div class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; text-align: justify; font-size: 14px;\">Xóa tất cả các khoản nợ tồn đọng trong 6 tháng</div>                    </div>                </div>                <div id=\"cv-item-work-experience\" class=\"cv-item movable padding\">                    <div class=\"cv-item-header\">                        <div class=\"cv-item-content\" contenteditable=\"true\" onclick=\"onClickElement(this)\">                            <b>KINH NGHIỆM LÀM VIỆC</b>                        </div>                    </div>                    <div class=\"cv-item-body movable copyable\">                        <div style=\"display: flex; justify-content: space-between;\">                            <span class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; font-size: 14px;\">                                <b>PRODUCT MANAGER</b>                            </span>                            <span class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; font-size: 14px;\"><b>03/2017 - 03/2018</b></span>                        </div>                        <div class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"margin-top: -20px; font-size: 14px;\">                            <b><i>Digital Innovation</i></b>                        </div>                        <div class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"margin-top: -20px; font-size: 14px; text-align: justify; line-height: 25px;\">                            Cung cấp thông tin, định hướng và hỗ trợ nhóm Agile trong quá trình phát triển phần                            mềm:                            <ul>                                <li>Làm việc với người dùng/ khách hàng, các bên liên quan và nhóm delivery để                                    thu thập thông tin.</li>                                <li>Thảo luận với developer, tester và BA để làm rõ và đảm bảo chức năng phù hợp                                    với mong đợi của người dùng.</li>                                <li>Chịu trách nhiệm tạo, lên danh sách và sắp xếp thứ tự ưu tiên của backlog                                    cho sản phẩm web.</li>                                <li>Làm việc với Project Manager để lên kế hoạch, chương trình dự phòng, đảm bảo                                    sản phẩm đúng với tầm nhìn và lộ trình.</li>                            </ul>                        </div>                    </div>                    <div class=\"cv-item-body movable copyable\">                        <div style=\"display: flex; justify-content: space-between;\">                            <span class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; font-size: 14px;\">                                <b>BUSINESS ANALYST</b>                            </span>                            <span class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"line-height: 24px; font-size: 14px;\"><b>02/2016 - 03/2017</b></span>                        </div>                        <div class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"margin-top: -20px; font-size: 14px;\">                            <b><i>Insign Group</i></b>                        </div>                        <div class=\"cv-item-content padding\" contenteditable=\"true\" onclick=\"onClickElement(this)\" style=\"margin-top: -20px; font-size: 14px; text-align: justify; line-height: 25px;\">                            Dựa trên các thông tin từ người dùng, khách hàng và Product owner, tiến hành phân                            tích và làm việc cùng nhóm Agile để phát triển sản phẩm web:                            <ul>                                <li>Làm việc trực tiếp với người dùng cuối để tìm hiểu và phân tích những khó                                    khăn khi sử dụng sản phẩm.</li>                                <li>Phối hợp với developer và tester để cải thiện UI/UX và logic cho các chức                                    năng của sản phẩm.</li>                                <li>Chịu trách nhiệm về phát triển cải tiến liên tục, tạo và sắp xếp các story                                    sau khi thảo luận.</li>                                <li>Sắp xếp mức độ ưu tiên làm việc cho nhóm Agile và xem xét các backlog còn                                    lại.</li>                                <li>Báo cáo KPI Delivery với Project Manager và CTO.</li>                            </ul>                        </div>                    </div>                </div>            </div>        </div>          <div class=\"d-flex\" id=\"rowAddRow\">            <button id=\"cv-btn-add-row\" onclick=\"editRow(this, 0)\">                <i class=\"fa-solid fa-plus\"></i>                Thêm hàng            </button>        </div>    '",
            //    Name = "",
            //};
            //return new ViewAsPdf(newItem);

            return View();
        }
    }
}
