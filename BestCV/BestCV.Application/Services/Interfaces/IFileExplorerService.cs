using BestCV.Application.Models.FolderUploads;
using BestCV.Application.Models.UploadFiles;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.UploadFiles;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
	public interface IFileExplorerService
	{
		/// <summary>
		/// Author: TUNGTD
		/// Created: 30/07/2023
		/// Description: Get all folder
		/// </summary>
		/// <returns></returns>
		Task<DionResponse> GetAllFolders();
		/// <summary>
		/// Author: TUNGTD
		/// Created: 30/07/2023
		/// Description: Add folder
		/// </summary>
		/// <param name="obj">insert folder upload DTO</param>
		/// <returns></returns>
		Task<DionResponse> AddFolder(InsertFolderUploadDTO obj);
		/// <summary>
		/// Author: TUNGTD
		/// Created: 31/07/2023
		/// Description: Save file to server
		/// </summary>
		/// <param name="obj">insert upload file object</param>
		/// <returns></returns>
		Task<DionResponse> SaveFile(InsertUploadFileDTO obj);
		/// <summary>
		/// Author: TUNGTD
		/// Created: 31/07/2023
		/// Description: save large file
		/// </summary>
		/// <param name="obj">insert larger upload file DTO object</param>
		/// <returns></returns>
		Task<DionResponse> SaveLargeFile(InsertLargeUploadFileDTO obj);
		/// <summary>
		/// Author: TUNGTD
		/// Created: 31/07/2023
		/// Description: get list paging upload file
		/// </summary>
		/// <param name="parameters">paging upload file parameters</param>
		/// <returns></returns>
		Task<DionResponse> ListPagingFile(PagingUploadFileParameter parameters);
		/// <summary>
		/// Author: TUNGTD
		/// Created: 01/08/2023
		/// Description: Current usre upload file
		/// </summary>
		/// <param name="obj">insert upload file DTO</param>
		/// <param name="user">type user upload</param>
		/// <param name="type">type file upload</param>
		/// <returns></returns>
		Task<DionResponse> CurrentUserSaveFile(InsertUploadFileDTO obj, string user, string type);
	}
}
