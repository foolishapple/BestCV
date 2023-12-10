using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.UploadFiles
{
    public class InsertUploadFileDTO
    {
        /// <summary>
        /// Mã FolderUpload
        /// </summary>
        public int FolderUploadId { get; set; }
        /// <summary>
        /// Mã tài khoản admin
        /// </summary>
        public long AdminAccountId { get; set; }
        /// <summary>
        /// Tệp tin đẩy lên
        /// </summary>
        public ICollection<IFormFile> Files { get; set; } = default!;
    }
}
