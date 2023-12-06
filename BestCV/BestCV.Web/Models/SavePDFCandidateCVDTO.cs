namespace Jobi.Web.Models
{
    public class SavePDFCandidateCVDTO
    {
        /// <summary>
        /// Tên CV
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// HTML của CV
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// CSS bổ sung của CV
        /// </summary>
        public string AdditionalCSS { get; set; } = null!;
    }
}
