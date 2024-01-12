using BestCV.Application.Models.UploadFiles;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace BestCV.Application.Services.Implement
{
	public class FileStorageService : IFileStorageService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public FileStorageService(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}


		public string AddFolder(string folderPath)
		{
			int count = 1;
			string folderName = Path.GetFileNameWithoutExtension(folderPath);
			string parentDirectory = Path.GetDirectoryName(folderPath);
			string newFolderName = folderName;
			while (!Directory.Exists(folderPath))
			{
				newFolderName = string.Format("{0}({1})", folderName, count++);
				folderPath = Path.Combine(parentDirectory, newFolderName);
			}
			Directory.CreateDirectory(folderPath);
			return folderPath;
		}

		public Task<UploadFile> SaveFile(IFormFile file, string filePath)
		{
			throw new NotImplementedException();
		}

		public async Task<UploadFile> SaveImage(IFormFile file, string filePath)
		{
			using (var image = Image.Load(file.OpenReadStream()))
			{
				if (image.Width > UploadFileConst.MAXIMUM_IMAGE_WIDTH)//resize file img
				{
					float scale = MathF.Ceiling(image.Width / UploadFileConst.MAXIMUM_IMAGE_WIDTH);
					int newHeight = (int)MathF.Ceiling(image.Height / scale);
					image.Mutate(h => h.Resize(UploadFileConst.MAXIMUM_IMAGE_WIDTH, newHeight));
				}
				await image.SaveAsync(filePath);
				string thumbPath = await CreateThumbnail(filePath, image);
				return new UploadFile()
				{
					ThumbnailPath = thumbPath.Replace(GetRootFolderPath(),"").GetWebFormatPath()
				};
			}
		}


		public async Task<string> CreateThumbnail(string filePath, Image image)//funciton only run in using stream or file
		{
			string rootPath = Path.Combine(Path.GetDirectoryName(filePath), FolderUploadConst.FOLDER_THUMB_NAME);
			string fileName = Path.GetFileName(filePath);
			var newFullPath = Path.Combine(rootPath, fileName);
			string newThumbName = fileName;
			string newThumnailsPath = newFullPath;
			Image thumb = image;
			int count = 1;
			if (!Directory.Exists(rootPath))
			{
				Directory.CreateDirectory(rootPath);
			}
			if (thumb.Width > UploadFileConst.MAXIMUN_THUMB_WIDTH)
			{
				float scale = MathF.Ceiling(image.Width / UploadFileConst.MAXIMUN_THUMB_WIDTH);
				int newHeight = (int)MathF.Ceiling(thumb.Height / scale);
				thumb.Mutate(h => h.Resize(UploadFileConst.MAXIMUN_THUMB_WIDTH, newHeight));
			}
			while (File.Exists(newFullPath))
			{
				newThumbName = string.Format("{0}({1})", newThumbName, count++);
				newThumnailsPath = Path.Combine(rootPath, newThumbName);
			}
			await thumb.SaveAsync(newThumnailsPath);
			return newThumnailsPath;
		}

		public async Task<List<UploadFile>> SaveLargeFile(InsertLargeUploadFileDTO obj, string rootFolder)
		{
			List<UploadFile> result = new();
			string uploadFolder = obj.FolderRoot;
			if (!Directory.Exists(uploadFolder))
			{
				Directory.CreateDirectory(uploadFolder);
			}
			var boundary = GetBoundary(MediaTypeHeaderValue.Parse(obj.ContentType));
			if (obj.Stream == null)
			{
				throw new Exception("Stream not null");
			}
			var multipartReader = new MultipartReader(boundary, obj.Stream);
			var section = await multipartReader.ReadNextSectionAsync();
			while (section != null)
			{
				var fileSection = section.AsFileSection();
				if (fileSection != null)
				{
					var path = Path.Combine(uploadFolder, fileSection.FileName);
					int count = 1;
					string fileNameOnly = Path.GetFileNameWithoutExtension(path);
					string extension = Path.GetExtension(path);
					string newFullPath = path;
					string newFileName = fileNameOnly;
					while (File.Exists(newFullPath))
					{
						newFileName = string.Format("{0}({1})", fileNameOnly, count++);
						newFullPath = Path.Combine(uploadFolder, newFileName + extension);
					}
					await SaveLagreFileAsync(fileSection, newFullPath);
					var contentType = fileSection.Section.ContentType;
					FileInfo fileInfo = new FileInfo(newFullPath);
					var file = new UploadFile()
					{
						Size = fileInfo.Length,
						FolderUploadId = obj.FolderUploadId,
						AdminAccountId = obj.AdminUploadId,
						Active = true,
						CreatedTime = DateTime.Now,
						Extension = fileInfo.Extension,
						Name = fileInfo.Name,
						Path = newFullPath.Replace(_webHostEnvironment.WebRootPath, "").Replace(@"\", "/"),
						MimeType = contentType,
						ThumbnailPath = UploadFileConst.DEFAULT_THUMB
					};
					result.Add(file);
				}
				section = await multipartReader.ReadNextSectionAsync();
			}
			return result.ToList();
		}

		public async Task<List<UploadFile>> SaveManyFile(InsertUploadFileDTO obj,string rootPath)
		{
			List<UploadFile> result = new ();
			if (!Directory.Exists(rootPath))
			{
				Directory.CreateDirectory(rootPath);//created new folder if folder is not existed
			}
			foreach (var item in obj.Files)
			{
				var path = Path.Combine(rootPath, item.FileName);
				int count = 1;
				string fileNameOnly = Path.GetFileNameWithoutExtension(path);
				string extension = Path.GetExtension(path);
				string newFullPath = path;
				string newFileName = fileNameOnly;
				string thumbPath = "";
				while (File.Exists(newFullPath))
				{
					newFileName = string.Format("{0}({1})", fileNameOnly, count++);
					newFullPath = Path.Combine(rootPath, newFileName + extension);
				}
				if (UploadFileConst.FILE_IMG_EXTENSION.Contains(extension))
				{
					thumbPath = (await SaveImage(item, newFullPath)).ThumbnailPath;
				}
				else
				{
					using (var stream = new FileStream(newFullPath, FileMode.Create))
					{
						await item.CopyToAsync(stream);
					}
				}
				FileInfo fileInfo = new FileInfo(newFullPath);
				var file = new UploadFile()
				{
					Size = fileInfo.Length,
					FolderUploadId = obj.FolderUploadId,
					AdminAccountId = obj.AdminAccountId==0? AdminAccountConst.SUPER_ADMIN_ID: obj.AdminAccountId,
					Active = true,
					CreatedTime = DateTime.Now,
					Extension = fileInfo.Extension,
					Name = fileInfo.Name,
					Path = newFullPath.Replace(GetRootFolderPath(), "").Replace(@"\", "/"),
					MimeType = item.ContentType,
					ThumbnailPath = string.IsNullOrEmpty(thumbPath)? UploadFileConst.DEFAULT_THUMB : thumbPath
				};
				result.Add(file);
			}
			return result;
		}

		public string GetBoundary(MediaTypeHeaderValue contentType)
		{
			var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

			if (string.IsNullOrWhiteSpace(boundary))
			{
				throw new InvalidDataException("Missing content-type boundary.");
			}
			return boundary;
		}

		public async Task<long> SaveLagreFileAsync(FileMultipartSection fileSection, string filePath)
		{
			await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024);
			await fileSection.FileStream?.CopyToAsync(stream);
			return fileSection.FileStream.Length;
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

		public async Task<List<UploadFile>> SaveCoverPhoto(InsertUploadFileDTO obj, string rootPath)
		{
			List<UploadFile> result = new();
			if (!Directory.Exists(rootPath))
			{
				Directory.CreateDirectory(rootPath);//created new folder if folder is not existed
			}
			foreach (var item in obj.Files)
			{
				var path = Path.Combine(rootPath, item.FileName);
				int count = 1;
				string fileNameOnly = Path.GetFileNameWithoutExtension(path);
				string extension = Path.GetExtension(path);
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileNameOnly;
                string newFullPath = Path.Combine(rootPath, newFileName+ extension);
                while (File.Exists(newFullPath))
				{
					newFileName = string.Format("{0}({1})", fileNameOnly, count++);
					newFullPath = Path.Combine(rootPath, newFileName + extension);
				}
				using (var image = Image.Load(item.OpenReadStream()))
				{
					if (image.Width > UploadFileConst.MAXIMUM_COVER_PHOTO_WIDTH)//resize file img
					{
						float scale = MathF.Ceiling(image.Width / UploadFileConst.MAXIMUM_IMAGE_WIDTH);
						int newHeight = (int)MathF.Ceiling(image.Height / scale);
						image.Mutate(h => h.Resize(UploadFileConst.MAXIMUM_IMAGE_WIDTH, newHeight));
					}
					await image.SaveAsync(newFullPath);
				}
				FileInfo fileInfo = new FileInfo(newFullPath);
				var file = new UploadFile()
				{
					Size = fileInfo.Length,
					FolderUploadId = obj.FolderUploadId,
					AdminAccountId = obj.AdminAccountId == 0 ? AdminAccountConst.SUPER_ADMIN_ID : obj.AdminAccountId,
					Active = true,
					CreatedTime = DateTime.Now,
					Extension = fileInfo.Extension,
					Name = fileInfo.Name,
					Path = newFullPath.Replace(GetRootFolderPath(), "").Replace(@"\", "/"),
					MimeType = item.ContentType,
					ThumbnailPath = UploadFileConst.DEFAULT_THUMB
				};
				result.Add(file);
			}
			return result;
		}

		public async Task<List<UploadFile>> SaveAvartar(InsertUploadFileDTO obj, string rootPath)
		{
			List<UploadFile> result = new();
			if (!Directory.Exists(rootPath))
			{
				Directory.CreateDirectory(rootPath);//created new folder if folder is not existed
			}
			foreach (var item in obj.Files)
			{
				var path = Path.Combine(rootPath, item.FileName);
				int count = 1;
				string fileNameOnly = Path.GetFileNameWithoutExtension(path);
				string extension = Path.GetExtension(path);
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileNameOnly;
                string newFullPath = Path.Combine(rootPath, newFileName + extension);
                while (File.Exists(newFullPath))
				{
					newFileName = string.Format("{0}({1})", fileNameOnly, count++);
					newFullPath = Path.Combine(rootPath, newFileName + extension);
				}
				using (var image = Image.Load(item.OpenReadStream()))
				{
					if (image.Width > UploadFileConst.MAXIMUM_AVARTAR_WIDTH)//resize file img
					{
						float scale = MathF.Ceiling(image.Width / UploadFileConst.MAXIMUM_IMAGE_WIDTH);
						int newHeight = (int)MathF.Ceiling(image.Height / scale);
						image.Mutate(h => h.Resize(UploadFileConst.MAXIMUM_IMAGE_WIDTH, newHeight));
					}
					await image.SaveAsync(newFullPath);
				}
				FileInfo fileInfo = new FileInfo(newFullPath);
				var file = new UploadFile()
				{
					Size = fileInfo.Length,
					FolderUploadId = obj.FolderUploadId,
					AdminAccountId = obj.AdminAccountId == 0 ? AdminAccountConst.SUPER_ADMIN_ID : obj.AdminAccountId,
					Active = true,
					CreatedTime = DateTime.Now,
					Extension = fileInfo.Extension,
					Name = fileInfo.Name,
					Path = newFullPath.Replace(GetRootFolderPath(), "").Replace(@"\", "/"),
					MimeType = item.ContentType,
					ThumbnailPath = UploadFileConst.DEFAULT_THUMB
				};
				result.Add(file);
			}
			return result;
		}
	}
}
