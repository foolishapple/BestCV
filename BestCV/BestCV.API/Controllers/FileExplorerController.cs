using BestCV.API.Utilities;
using BestCV.Application.Models.FolderUploads;
using BestCV.Application.Models.UploadFiles;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Helpers;
using BestCV.Domain.Aggregates.UploadFiles;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
	[ApiController]
	[Route("api/file-explorer")]
	public class FileExplorerController : BaseController
	{
		private readonly IFileExplorerService _fileExplorerService;
		private readonly ILogger _logger;
		public FileExplorerController(IFileExplorerService fileExplorerService, ILoggerFactory loggerFactory)
		{
			_fileExplorerService = fileExplorerService;
			_logger = loggerFactory.CreateLogger<FileExplorerController>();
		}

		[HttpPost("add-folder")]		
		public async Task<IActionResult> AddFolder([FromBody]InsertFolderUploadDTO obj)
		{
			try
			{
				var response = await _fileExplorerService.AddFolder(obj);
				return Ok(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to add folder.");
				return BadRequest();
			}
		}

		[HttpGet("list-all-folder")]		
		public async Task<IActionResult> ListAllFolder()
		{
			try
			{
				var response = await _fileExplorerService.GetAllFolders();
				return Ok(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to get list all folder upload");
				return BadRequest();
			}
		}

		[HttpPost("upload-file")]
		[MultipartFormData]
		public async Task<IActionResult> UploadFile([FromForm] InsertUploadFileDTO obj)
		{
			try
			{
				var response = await _fileExplorerService.SaveFile(obj);
				return Ok(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to upload file to server");
				return BadRequest();
			}
		}

		[HttpPost("upload-large-file/{id}")]
		[MultipartFormData]
		[DisableFormValueModelBinding]
		public async Task<IActionResult> UploadLargeFile(int id)
		{
			try
			{
				var accountId = this.GetLoggedInUserId();
				if (accountId == 0)
				{
					accountId = AdminAccountConst.SUPER_ADMIN_ID;
				}
				var obj = new InsertLargeUploadFileDTO()
				{
					Stream = HttpContext.Request.Body,
					AdminUploadId = accountId,
					ContentType = HttpContext.Request.ContentType,
					FolderRoot = "",
					FolderUploadId = id
				};
				var response = await _fileExplorerService.SaveLargeFile(obj);
				return Ok(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to upload file to server");
				return BadRequest();
			}
		}

		[HttpPost("list-upload-file")]
		public async Task<IActionResult> ListUploadFile([FromBody] PagingUploadFileParameter parameters)
		{
			try
			{
				var response = await _fileExplorerService.ListPagingFile(parameters);
				return Ok(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to get list paging upload file.");
				return BadRequest();
			}
		}

		[HttpPost("upload/{user}/{type}")]		
		public async Task<IActionResult> Upload([FromForm] InsertUploadFileDTO obj, string user, string type)
		{
			try
			{

				if (!FolderUploadConst.MAPPING.ContainsKey(user))
				{
					return NotFound();
				}
				else if (!FolderUploadConst.MAPPING[user].ContainsKey(type))
				{
					return NotFound();
				}
				obj.AdminAccountId = AdminAccountConst.SUPER_ADMIN_ID;
				obj.FolderUploadId = FolderUploadConst.MAPPING[user][type];
				var response = await _fileExplorerService.CurrentUserSaveFile(obj,user,type);
				return Ok(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to upload file by another user");
				return BadRequest();
			}
		}
	}
}
