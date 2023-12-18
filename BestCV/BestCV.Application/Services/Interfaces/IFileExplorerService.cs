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

		Task<BestCVResponse> GetAllFolders();

		Task<BestCVResponse> AddFolder(InsertFolderUploadDTO obj);

		Task<BestCVResponse> SaveFile(InsertUploadFileDTO obj);

		Task<BestCVResponse> SaveLargeFile(InsertLargeUploadFileDTO obj);
	
		Task<BestCVResponse> ListPagingFile(PagingUploadFileParameter parameters);

		Task<BestCVResponse> CurrentUserSaveFile(InsertUploadFileDTO obj, string user, string type);
	}
}
