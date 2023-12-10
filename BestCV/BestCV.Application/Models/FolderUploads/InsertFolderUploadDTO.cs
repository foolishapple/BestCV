using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.FolderUploads
{
	public class InsertFolderUploadDTO
	{
		/// <summary>
		/// Mã thư mục cha
		/// </summary>
		public int? ParentId { get; set; }
		/// <summary>
		/// Tên thư mục
		/// </summary>
		public string Name { get; set; } = null!;

		public string? Description { get; set; }
	}
}
