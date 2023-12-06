namespace Jobi.Storage.Models
{
    public class UploadLagreFilesVM
    {
        /// <summary>
        /// Request body
        /// </summary>
        public Stream? Stream { get; set; }
        /// <summary>
        /// Request content type
        /// </summary>
        public string ContentType { get; set; } = null!;
        /// <summary>
        /// Folder upload id
        /// </summary>
        public int FolderId { get; set; }
        /// <summary>
        /// Folder upload path
        /// </summary>
        public string FolderPath { get; set; } = null!;
        /// <summary>
        /// Account upload Id
        /// </summary>
        public long AccountId { get; set; }
    }
}
