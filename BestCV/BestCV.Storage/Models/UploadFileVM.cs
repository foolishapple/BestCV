namespace BestCV.Storage.Models
{
    public class UploadFileVM
    {
        /// <summary>
        /// Mã người tải file lên
        /// </summary>
        public long UpdaterId { get; set; }
        /// <summary>
        /// Danh sách file đẩy lên
        /// </summary>
        public ICollection<IFormFile> Files { get; set; } = default!;
        public string FolderPath { get; set; } = null!;
        /// <summary>
        /// Mã folder
        /// </summary>
        public int FolderId { get; set; }
    }
}
