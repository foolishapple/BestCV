using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class FolderUpload:EntityCommon<int>
    {
        /// <summary>
        /// Đường dẫn thư mục
        /// </summary>
        public string Path { get; set; } = null!;
        /// <summary>
        /// Mã thư mục cha
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// Mã cây thư mục
        /// </summary>
        public string TreeIds { get; set; } = null!;

        public virtual ICollection<UploadFile> UploadFiles { get; } = new List<UploadFile>();
    }
}
