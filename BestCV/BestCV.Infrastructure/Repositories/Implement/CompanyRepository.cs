using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Candidate;
using BestCV.Domain.Aggregates.Company;
using BestCV.Domain.Aggregates.CompanyFieldOfActivity;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CompanyRepository : RepositoryBaseAsync<Company, int, JobiContext>, ICompanyRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public CompanyRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime: 09/08/2023
        /// Description: get detail company by id
        /// </summary>
        /// <param name=companyId></param>
        /// <returns></returns>
        public async Task<CompanyAggregates> GetCompanyAggregatesById(int companyId)
        {
            var result = await(
                from c in db.Companies
                join cs in db.CompanySizes on c.CompanySizeId equals cs.Id
                join e in db.Employers on c.EmployerId equals e.Id


                where c.Active && cs.Active && e.Active && c.Id == companyId
                select new CompanyAggregates
                {
                    Id = c.Id,
                    EmployerId = c.EmployerId,
                    CompanySizeId = c.CompanySizeId,
                    CompanySizeName = cs.Name,
                    Name = c.Name,
                    Active = c.Active,
                    AddressDetail = c.AddressDetail,
                    CoverPhoto = c.CoverPhoto,
                    CreatedTime = c.CreatedTime,
                    Description = c.Description,
                    EmailAddress = c.EmailAddress,
                    FoundedIn = c.FoundedIn,
                    FacebookLink = c.FacebookLink,
                    TwitterLink = c.TwitterLink,
                    LinkedinLink = c.LinkedinLink,
                    Location = c.Location,
                    Logo = c.Logo,
                    Phone = c.Phone,
                    Search = c.Search,
                    TaxCode = c.TaxCode,
                    Website = c.Website,
                    Overview = c.Overview,
                }).FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime: 01/08/2023
        /// /// Description: get detail company by employerId
        /// </summary>
        /// <param name=employerId></param>
        /// <returns></returns>
        public async Task<Company> GetDetailByEmnployerId(long employerId)
        {
            var data = await db.Companies.FirstOrDefaultAsync(x => x.EmployerId == employerId && x.Active);
            return data;
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime: 01/08/2023
        /// /// Description: Check company name is Existed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> IsNameExist(string name)
        {
            var data = await db.Companies.AnyAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim() && x.Active);
            return data;
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime: 07/08/2023
        /// Description : Lấy danh sách công ty (server side)
        /// Updater : HuyDQ
        /// UpdatedTime : 17/08/2023
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns></returns>
        public async Task<object> ListCompanyAggregates(DTParameters parameters)
        {
            var keyword = parameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (parameters.Order != null)
            {
                orderCriteria = parameters.Columns[parameters.Order[0].Column].Data;
                orderAscendingDirection = parameters.Order[0].Dir.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = await (
                from c in db.Companies
                join cs in db.CompanySizes on c.CompanySizeId equals cs.Id
                join e in db.Employers on c.EmployerId equals e.Id
                join w in db.WorkPlaces on c.WorkPlaceId equals w.Id


                where c.Active && cs.Active && e.Active
                select new CompanyAggregates
                {
                    Id = c.Id,
                    EmployerId = c.EmployerId,
                    CompanySizeId = c.CompanySizeId,
                    CompanySizeName = cs.Name,
                    Name = c.Name,
                    Active = c.Active,
                    AddressDetail = c.AddressDetail,
                    WorkPlaceId = (int)c.WorkPlaceId,
                    WorkPlaceName = w.Name,
                    CoverPhoto = c.CoverPhoto,
                    CreatedTime = c.CreatedTime,
                    Description = c.Description,
                    EmailAddress = c.EmailAddress,
                    FoundedIn = c.FoundedIn,
                    Location = c.Location,
                    Logo = c.Logo,
                    Phone = c.Phone,
                    Search = c.Search,
                    TaxCode = c.TaxCode,
                    Website = c.Website,
                    Overview = c.Overview
                }).ToListAsync();

            var recordsTotal = result.Count;
            result = result.Where(s => string.IsNullOrEmpty(keyword) || s.Name.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.AddressDetail.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.Website.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.TaxCode.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())|| s.CreatedTime.ToCustomString().Contains(keyword) || (!string.IsNullOrEmpty(s.Search) && s.Search.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))).ToList();


            foreach (var column in parameters.Columns)
            {
                var search = column.Search.Value;
                if (!search.Contains("/"))
                {
                    search = column.Search.Value.ToLower().RemoveVietnamese();

                }
                if (string.IsNullOrEmpty(search)) continue;
                switch (column.Data)
                {
                    case "name":
                        result = result.Where(r => r.Name.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "taxCode":
                        result = result.Where(r => r.TaxCode.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "addressDetail":
                        result = result.Where(r => r.AddressDetail.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "website":
                        result = result.Where(r => r.Website.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "createdTime":
                        var searchDateArrs = search.Split(',');

                        if (searchDateArrs.Length == 2)
                        {
                            //Không có ngày bắt đầu
                            if (string.IsNullOrEmpty(searchDateArrs[0]))
                            {
                                result = result.Where(r => r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                            //không có ngày kết thúc
                            else if (string.IsNullOrEmpty(searchDateArrs[1]))
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0])).ToList();
                            }
                            //có cả 2
                            else
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0]) && r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                        }

                        break;

                }
            }
            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();
            var data = new
            {
                draw = parameters.Draw,
                recordsTotal = recordsTotal,
                allData = result,
                recordsFiltered = result.Count,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList()
            };
            return data;
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 17/08/2023
        /// Description : Tìm kiếm công ty trang chủ (server side)
        /// </summary>
        /// <param name="param">SearchingCompanyParameters</param>
        /// <returns></returns>
        public async Task<PagingData<List<CompanyAggregates>>> SearchCompanyHomePageAsync(SearchingCompanyParameters parameter)
        {
            //khai báo biến
            var keyword = parameter.Keywords;
            var pageIndex = parameter.PageIndex;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            var workPlace = parameter.WorkPlace;
            var companySize = parameter.CompanySize;
            var fieldOfActivity = parameter.FieldOfActivity;
            var pageSize = parameter.PageSize;
            var candidateId = parameter.CandidateId;


            if (parameter.OrderCriteria != null)
            {
                orderCriteria = parameter.OrderCriteria;
                orderAscendingDirection = parameter.OrderCriteria.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }
            //lấy dữ liệu
            var result = (
                from row in db.Companies
                join cs in db.CompanySizes on row.CompanySizeId equals cs.Id
                join w in db.WorkPlaces on row.WorkPlaceId equals w.Id
                join e in db.Employers on row.EmployerId equals e.Id
                
                where row.Active && cs.Active && w.Active && e.Active && w.Active
                && row.CompanySizeId == cs.Id
                && row.WorkPlaceId == w.Id
                && row.EmployerId == e.Id               
                select new CompanyAggregates
                {
                    Id = row.Id,
                    EmployerId = row.EmployerId,
                    CompanySizeId = row.CompanySizeId,
                    CompanySizeName = cs.Name,
                    Name = row.Name,
                    Active = row.Active,
                    AddressDetail = row.AddressDetail,
                    WorkPlaceId = row.WorkPlaceId,
                    WorkPlaceName = w.Name,
                    CoverPhoto = row.CoverPhoto,
                    CreatedTime = row.CreatedTime,
                    Description = row.Description,
                    EmailAddress = row.EmailAddress,
                    FoundedIn = row.FoundedIn,
                    Location = row.Location,
                    Logo = row.Logo,
                    Phone = row.Phone,
                    Search = row.Search,
                    TaxCode = row.TaxCode,
                    Website = row.Website,
                    LinkedinLink = row.LinkedinLink,
                    FacebookLink = row.FacebookLink,
                    TwitterLink = row.TwitterLink,
                    Overview = row.Overview,
                    companyFieldOfActivityAggregates = (from cf in db.CompanyFieldOfActivities
                                      from c in db.Companies
                                      from f in db.FieldOfActivities
                                      where cf.Active && c.Active && f.Active && cf.CompanyId == c.Id && cf.CompanyId == row.Id && cf.FieldOfActivityId == f.Id
                                      select new CompanyFieldOfActivityAggregates
                                      {
                                          Id = cf.Id,
                                          CompanyId = cf.CompanyId,
                                          FieldOfActivityId = cf.FieldOfActivityId,
                                          Active = cf.Active,
                                          FieldOfActivityName = f.Name,
                                          CreatedTime = cf.CreatedTime,
                                          CompanyName = c.Name
                                      }).ToList(),
                    CountJob = (from row1 in db.Jobs
                                from jc in db.JobCategories
                                from js in db.JobStatuses
                                from jt in db.JobTypes
                                from jp in db.JobPosition
                                from er in db.ExperienceRange
                                from st in db.SalaryType
                                from e in db.Employers
                                from com in db.Companies
                                from rc in db.RecruitmentCampaigns
                                where row1.Active && jc.Active && js.Active && jt.Active && jp.Active && st.Active && er.Active && e.Active && com.Active && rc.Active
                                && row1.PrimaryJobCategoryId == jc.Id
                                && row1.JobStatusId == js.Id
                                && row1.JobTypeId == jt.Id
                                && row1.JobPositionId == jp.Id
                                && row1.ExperienceRangeId == er.Id
                                && row1.SalaryTypeId == st.Id
                                && row1.RecruimentCampaignId == rc.Id
                                && com.EmployerId == rc.EmployerId
                                && rc.EmployerId == e.Id
                                && row1.IsApproved
                                && rc.IsAprroved && e.Id == row.EmployerId
                                select row).Count(),
                    IsFollowedCompany = db.CandidateFollowCompanies.Any(x => x.Active && x.CandidateId == candidateId && x.CompanyId == row.Id),

                });

            var allRecord = await result.CountAsync();

            //tìm kiếm tất cả
            //result = result.Where(s => string.IsNullOrEmpty(keyword) || 
            //                      s.Name.RemoveVietnamese().ToLower().Contains(keyword.RemoveVietnamese().ToLower()) ||
            //                      s.WorkPlaceName.RemoveVietnamese().ToLower().Contains(keyword.RemoveVietnamese().ToLower()) ||
            //                      s.CompanySizeName.RemoveVietnamese().ToLower().Contains(keyword.RemoveVietnamese().ToLower()) ||
            //                      s.companyFieldOfActivityAggregates.Any(x => x.FieldOfActivityName.RemoveVietnamese().ToLower().Contains(keyword.RemoveVietnamese().ToLower())) ||
            //                      s.Search.RemoveVietnamese().ToLower().Contains(keyword.RemoveVietnamese().ToLower()));

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(s=> 
                EF.Functions.Collate(s.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)));
            }
            
            //tìm kiếm theo địa điểm
            if (workPlace != 0)
            {
                result = result.Where(s => s.WorkPlaceId == workPlace);
            }
            
            //tìm kiếm theo quy mô công ty
            if (companySize.Length != 0)
            {
                var companySizeArr = companySize.Split(",").Select(n => Int64.Parse(n));
                result = result.Where(r => companySizeArr.Contains(r.CompanySizeId));
                //result = result.Where(s => s.CompanySizeId.ToString().RemoveVietnamese().ToLower().Contains(companySize.RemoveVietnamese().ToLower()));
            }

            //tìm kiếm theo lĩnh vực hoạt động
            if (fieldOfActivity != 0)
            {
                result = result.Where(s => s.companyFieldOfActivityAggregates.Any(x => x.FieldOfActivityId == fieldOfActivity));
            }

            //orderby
            if (parameter.OrderCriteria != null)
            {
                switch (parameter.OrderCriteria)
                {
                    case "asc":
                        result = result.OrderBy(x => x.Id);
                        break;
                    case "desc":
                        result = result.OrderByDescending(x => x.Id);
                        break;
                }
            }
            var recordsTotal = await result.CountAsync();
            return new PagingData<List<CompanyAggregates>>
            {
                DataSource = result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }
    }
}
