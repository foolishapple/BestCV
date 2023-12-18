using BestCV.Application.Models.FolderUploads;
using BestCV.Application.Models.UploadFiles;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.UploadFiles;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
	public class FileExplorerService : IFileExplorerService
	{
		private readonly IFolderUploadRepository _folderUploadRepository;
		private readonly IUploadFileRepository _uploadFileRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUnitOfWork<JobiContext> _unitOfWork;
		private readonly IFileStorageService _fileStorageService;
		private readonly ILogger _logger;
		public FileExplorerService(IFolderUploadRepository folderUploadRepository, IWebHostEnvironment webHostEnvironment, IUploadFileRepository uploadFileRepository, IUnitOfWork<JobiContext> unitOfWork, ILoggerFactory loggerFactory, IFileStorageService fileStorageService)
		{
			_folderUploadRepository = folderUploadRepository;
			_webHostEnvironment = webHostEnvironment;
			_uploadFileRepository = uploadFileRepository;
			_unitOfWork = unitOfWork;
			_logger = loggerFactory.CreateLogger<FileExplorerService>();
			_fileStorageService = fileStorageService;
		}

		public async Task<BestCVResponse> AddFolder(InsertFolderUploadDTO obj)
		{
			FolderUpload? parent = null;
			List<string> errors = new();
			string rootFolderPath = GetRootFolderPath();
			if (obj.ParentId != null)
			{
				parent = await _folderUploadRepository.GetByIdAsync((int)obj.ParentId);
				if (parent == null)
				{
					errors.Add("Mã thư mục cha không chính xác.");
					return BestCVResponse.BadRequest(errors);
				}
			}
			string newFolderName = obj.Name.GetValidFolderName();
			string newFolderPath = "";
			if(parent != null)
			{
				newFolderPath = Path.Combine(rootFolderPath + parent.Path.Replace("/",@"\"), newFolderName);
			}
			else
			{
				newFolderPath = Path.Combine(rootFolderPath, newFolderName);
			}
			if (!Directory.Exists(newFolderPath))
			{
				Directory.CreateDirectory(newFolderPath);
			}
			FolderUpload model = new()
			{
				Active = true,
				CreatedTime = DateTime.Now,
				Name = Path.GetFileName(newFolderPath),
				Description = obj.Description,
				ParentId = obj.ParentId,
				Path = newFolderPath.Replace(rootFolderPath,"").Replace(@"\","/"),
				TreeIds = parent != null ? $"{parent.TreeIds}_{parent.Id}" : ""
			};
			await _folderUploadRepository.CreateAsync(model);
			await _folderUploadRepository.SaveChangesAsync();
			return BestCVResponse.Success(obj);
		}

		public async Task<BestCVResponse> GetAllFolders()
		{
			var data = await _folderUploadRepository.GetAllAsync();
			foreach(var item in data)
			{
				item.TreeIds = $"{item.TreeIds}_{item.Id}";
			}
			data.OrderByDescending(c => c.TreeIds);
			return BestCVResponse.Success(data);
		}

		public async Task<BestCVResponse> SaveFile(InsertUploadFileDTO obj)
		{
			var folder = await _folderUploadRepository.GetByIdAsync(obj.FolderUploadId);
			if (folder == null)
			{
				throw new Exception("Folder not found");
			}
			var rootFolder = GetRootFolderPath() + folder.Path.GetFolderFormatPath();
			var models = await _fileStorageService.SaveManyFile(obj,rootFolder);
			await _uploadFileRepository.CreateListAsync(models);
			await _uploadFileRepository.SaveChangesAsync();
			return BestCVResponse.Success(models);
		}

		public async Task<BestCVResponse> SaveLargeFile(InsertLargeUploadFileDTO obj)
		{
			var folder = await _folderUploadRepository.GetByIdAsync(obj.FolderUploadId);
			if (folder == null)
			{
				throw new Exception("Folder not found");
			}
			var rootFolder = GetRootFolderPath() + folder.Path.GetFolderFormatPath();
			obj.FolderRoot = rootFolder;
			var models = await _fileStorageService.SaveLargeFile(obj, rootFolder);
			await _uploadFileRepository.CreateListAsync(models);
			await _uploadFileRepository.SaveChangesAsync();
			return BestCVResponse.Success();
		}


		private string GetRootFolderPath()
		{
			string rootPath = _webHostEnvironment.ContentRootPath;
			rootPath = rootPath.Replace(FolderUploadConst.ROOT_API_NAME, FolderUploadConst.ROOT_STORAGE_NAME);
			if (!rootPath.Contains(FolderUploadConst.ROOT_WEB_NAME))
			{
				rootPath = Path.Combine(rootPath, FolderUploadConst.ROOT_WEB_NAME);
			}
			return rootPath;
		}

		public async Task<BestCVResponse> ListPagingFile(PagingUploadFileParameter parameters)
		{
			var data = await _uploadFileRepository.ListPaging(parameters);
			return BestCVResponse.Success(data);
		}

		public async Task<BestCVResponse> CurrentUserSaveFile(InsertUploadFileDTO obj, string user, string type)
		{

            if (obj.Files.Any(c=>c.Length > UploadFileConst.MAXIMUM_UPLOAD_SIZE))
			{
				return BestCVResponse.BadRequest("Dung lượng tối đa của tệp tin tải lên là 5MB");
			}
			var folder = await _folderUploadRepository.GetByIdAsync(obj.FolderUploadId);
			if (folder == null)
			{
				throw new Exception("Folder not found");
			}
			var rootFolder = GetRootFolderPath() + folder.Path.GetFolderFormatPath();
			List<UploadFile> data = new();
			switch (type)
			{
				case FolderUploadConst.AVATAR_TYPE:
					data = await _fileStorageService.SaveAvartar(obj, rootFolder);
					break;
				case FolderUploadConst.COVER_PHOTO_TYPE:
					data = await _fileStorageService.SaveCoverPhoto(obj, rootFolder);
					break;
				default:
					data = await _fileStorageService.SaveManyFile(obj, rootFolder);
					break;
			}
			await _uploadFileRepository.CreateListAsync(data);
			await _uploadFileRepository.SaveChangesAsync();
			return BestCVResponse.Success(data);
		}
	}
}
