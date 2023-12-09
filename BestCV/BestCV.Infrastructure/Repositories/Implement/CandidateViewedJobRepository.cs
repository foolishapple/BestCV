using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using BestCV.Domain.Aggregates.CandidateViewedJob;
using BestCV.Domain.Aggregates.CandidateViewJobs;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CandidateViewedJobRepository : RepositoryBaseAsync<CandidateViewedJob, long, JobiContext>, ICandidateViewedJobRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public CandidateViewedJobRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 13/09/2023
        /// Description: datatable paging candidate viewed job parameter
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<DTResult<CandidateViewedJobAggreagate>> DTPaging(DTCandidateViewedJobParameters parameters)
        {
            //0.Option
            string orderCiteria = "Id";
            bool orderAscendingDirection = false;
            string keyword = "";
            string noneVietnameseKeyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
                noneVietnameseKeyword = parameters.Search.Value.Trim().ToLower().RemoveVietnamese();
            }
            if(parameters.Order!=null && parameters.Order.Length > 0)
            {
                orderCiteria = parameters.Columns[parameters.Order[0].Column].Data;
                orderAscendingDirection = parameters.Order[0].Dir == DTOrderDir.ASC;
            }
            //1.Query
            var query = from cvj in db.CandidateViewedJobs
                        join j in db.Jobs on cvj.JobId equals j.Id
                        join c in db.Candidates on cvj.CandidateId equals c.Id
                        where cvj.Active && j.Active && c.Active
                        select new CandidateViewedJobAggreagate()
                        {
                            CandidateEmail = c.Email,
                            CandidateId = c.Id,
                            CandidateName = c.FullName,
                            CandidatePhone = c.Phone,
                            CandidatePhoto = c.Photo,
                            CreatedTime = c.CreatedTime,
                            Id = cvj.Id,
                            JobId = j.Id
                        };
            if (parameters.JobId != null)
            {
                query = query.Where(c => c.JobId == parameters.JobId);
            }
            int recordsTotal = await query.CountAsync();
            //2.Fillter
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => EF.Functions.Collate(c.CandidateName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || c.CandidatePhone.Contains(noneVietnameseKeyword)
                || c.CandidateEmail.Contains(noneVietnameseKeyword)
                || c.CreatedTime.ToCustomString().Contains(noneVietnameseKeyword));
            }
            int recordsFiltered = await query.CountAsync();
            //3.Sort
            query = query.OrderByDynamic(orderCiteria, orderAscendingDirection ? LinqExtensions.Order.Asc : LinqExtensions.Order.Desc);
            //4.Return data
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            var result = new DTResult<CandidateViewedJobAggreagate>()
            {
                data = data,
                draw = parameters.Draw,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }


        public async Task<List<CandidateViewedJobAggregates>> ListCandidateViewedJobByCandidateId(long candidateId)
        {
            var query = from csj in db.CandidateViewedJobs
                        join j in db.Jobs on csj.JobId equals j.Id
                        join jc in db.JobCategories on j.PrimaryJobCategoryId equals jc.Id
                        join jt in db.JobTypes on j.JobTypeId equals jt.Id
                        join rc in db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join co in db.Companies on rc.EmployerId equals co.EmployerId
                        where csj.CandidateId == candidateId
                            && csj.Active
                            && j.Active
                            && jc.Active
                            && jt.Active
                            && rc.Active
                            && co.Active
                        select new CandidateViewedJobAggregates()
                        {
                            Id = csj.Id,
                            CompanyId = co.Id,
                            CompanyName = co.Name,
                            JobCategoryId = jc.Id,
                            JobCategoryName = jc.Name,
                            JobId = j.Id,
                            JobName = j.Name,
                            JobTypeId = jt.Id,
                            JobTypeName = jt.Name,
                            CreatedTime = csj.CreatedTime,
                        };
            return await query.ToListAsync();
        }

        public async Task<DTResult<CandidateViewedJobAggregates>> PagingByCandidateId(DTPagingCandidateViewedJobParameters parameters)
        {
            //0.Opttion
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }
            //1.Query LINQ
            var query = from cvj in db.CandidateViewedJobs
                        join j in db.Jobs on cvj.JobId equals j.Id
                        join c in db.Candidates on cvj.CandidateId equals c.Id
                        join rc in db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join em in db.Employers on rc.EmployerId equals em.Id
                        join com in db.Companies on em.Id equals com.EmployerId
                        where cvj.Active && j.Active && c.Active && rc.Active && em.Active && com.Active && j.IsApproved && cvj.CandidateId == parameters.CandidateId
                        orderby cvj.CreatedTime descending
                        select new CandidateViewedJobAggregates()
                        {
                            Id = cvj.Id,
                            CandidateId = cvj.CandidateId,
                            CompanyId = com.Id,
                            CompanyName = com.Name,
                            CreatedTime = cvj.CreatedTime,
                            JobId = cvj.JobId,
                            JobName = j.Name,
                            CompanyAddress = com.AddressDetail,
                            CompanyPhone = com.Phone,
                            CompanyWebsite = com.Website,
                            CityRequired = (from wp in db.WorkPlaces
                                            join rqc in db.JobRequireCities on wp.Id equals rqc.CityId
                                            where wp.Active && rqc.Active && rqc.JobId == j.Id
                                            select wp.Name).ToList(),
                            SalaryFrom = j.SalaryFrom,
                            SalaryTo = j.SalaryTo,
                            SalaryTypeId = j.SalaryTypeId,
                            JobApplyEndDate = j.ApplyEndDate,

                        };

            int recordsTotal = await query.CountAsync();

            if (!String.IsNullOrEmpty(keyword))
            {
                string noneVietnameseKeyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(c => EF.Functions.Collate(c.JobName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) || EF.Functions.Collate(c.CompanyName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) || c.CreatedTime.ToCustomString().Contains(noneVietnameseKeyword)
                || ((from wp in db.WorkPlaces
                     join rqc in db.JobRequireCities on wp.Id equals rqc.CityId
                     where wp.Active && rqc.Active && rqc.JobId == c.JobId && EF.Functions.Collate(wp.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                     select wp.Name).Count() > 0));
            }

            int recordsFiltered = await query.CountAsync();
            //3.Sort
            //4.Return data
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            DTResult<CandidateViewedJobAggregates> result = new()
            {
                draw = parameters.Draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }
    }
}
