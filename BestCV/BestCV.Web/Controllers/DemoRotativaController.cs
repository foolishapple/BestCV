using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace Jobi.Web.Controllers
{
    public class DemoRotativaController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;

        public DemoRotativaController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        public IActionResult DemoViewAsPDF()
        {
            return new ViewAsPdf("DemoViewAsPDF");
        }

        [HttpGet]
        public async Task<IActionResult> SaveAsPdf()
        {
            Rotativa.AspNetCore.ViewAsPdf viewAsPdf = new Rotativa.AspNetCore.ViewAsPdf("DemoViewAsPDF");
            byte[] pdfData = await viewAsPdf.BuildFile(ControllerContext);
            string fullPath = Path.Combine(_hostingEnvironment.WebRootPath, "pdf", "DemoRotativa1.pdf");
            using (var fileStream = new FileStream(fullPath.Replace("Jobi.Web","Jobi.Storage"), FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(pdfData, 0, pdfData.Length);
            }
            return Ok();
        }
    }
}
