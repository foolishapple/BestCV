using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.UploadFiles;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class UploadFileRepository : RepositoryBaseAsync<UploadFile,int,JobiContext>, IUploadFileRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public UploadFileRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
		/// <summary>
		/// Author: TUNGTD
		/// Created: 31/07/2023
		/// Description: get list paging upload file
		/// </summary>
		/// <param name="parameters">paging upload file parameters</param>
		/// <returns></returns>
		public async Task<PagingData<List<UploadFile>>> ListPaging(PagingUploadFileParameter parameters)
		{
			string keyword = parameters.Keyword.Trim();
			string contentType = parameters.ContentType.Trim();
			var query = from fi in _db.UploadFiles
						join fld in _db.FolderUploads on fi.FolderUploadId equals fld.Id
						where fi.Active && fld.Active && fld.Id == parameters.FodlderUploadId
						orderby fi.CreatedTime descending
						select fi;
			if (!String.IsNullOrEmpty(contentType))
			{
				string stringMime = "";
				var mimeAll = contentType.Split(",").Where(c => c.Contains("/*")).Select(c => c.Trim().Replace("*", ""));
				if (mimeAll != null && mimeAll.Count() > 0)
				{
					stringMime = String.Join(",", UploadFileConst._mappings.Where(c => mimeAll.Any(g => c.Value.Contains(g))).Select(c => c.Value));
				}
				query = query.Where(c => parameters.ContentType.Contains(c.MimeType) || parameters.ContentType.Contains(c.Extension) || stringMime.Contains(c.MimeType));
			}
			int totalRecord = await query.CountAsync();
			if (!string.IsNullOrEmpty(keyword))
			{
				keyword = keyword.ToLower().RemoveVietnamese();
				query = query.Where(c => c.Name.ToLower().Contains(keyword) || c.Extension.ToLower().Contains(keyword));
			}
			int totalFiltered = await query.CountAsync();
			var data = await query.Skip(parameters.PageStart).Take(parameters.PageSize).ToListAsync();
			var result = new PagingData<List<UploadFile>>()
			{
				DataSource = data,
				PageSize = parameters.PageSize,
				CurrentPage = parameters.PageIndex+1,
				Total =  totalRecord,
				TotalFiltered = totalFiltered,
			};
			return result;
		}
	}
}
