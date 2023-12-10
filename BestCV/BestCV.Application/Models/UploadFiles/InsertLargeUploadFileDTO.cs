using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.UploadFiles
{
	public class InsertLargeUploadFileDTO
	{
        /// <summary>
        /// mã thư mục upload
        /// </summary>
        public int FolderUploadId { get; set; }
        /// <summary>
        /// Mã quản trị viên upload tệp tin
        /// </summary>
        public long AdminUploadId { get; set; }

        /// <summary>
        /// Nguồn của tệp tin
        /// </summary>
        public Stream Stream { get; set; } = null!;
        /// <summary>
        /// Đường dẫn của tệp tin
        /// </summary>
		public string FolderRoot { get; set; } = null!;
        public string ContentType { get; set; } = null!;
    }
}
