using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateFollowCompany;
using BestCV.Domain.Aggregates.CandidateSaveJob;
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
    public class CandidateFollowCompanyRepository : RepositoryBaseAsync<CandidateFollowCompany, long, JobiContext>, ICandidateFollowCompanyRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public CandidateFollowCompanyRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<CandidateFollowCompany> GetAsyncByCandidateIdAndCompanyI(long candidateId, int companyId)
        {
            return await db.CandidateFollowCompanies.FirstOrDefaultAsync(x => x.CandidateId == candidateId && x.CompanyId == companyId);
        }

        public async Task<bool> IsCompanyIdExist(long candidateId, int companyId)
        {
            return await db.CandidateFollowCompanies.AnyAsync(s => s.CandidateId == candidateId && s.CompanyId == companyId);   
        }

        public async Task<List<CandidateFollowCompanyAggregates>> ListCandidateFollowCompanyByCandidateId(long candidateId)
        {
            var query = from clc in db.CandidateFollowCompanies
                        join c in db.Companies on clc.CompanyId equals c.Id
                        join cz in db.CompanySizes on c.CompanySizeId equals cz.Id
                        where clc.CandidateId == candidateId
                        && clc.Active
                        && c.Active
                        && cz.Active
                        select new CandidateFollowCompanyAggregates()
                        {
                            Id = clc.Id,
                            CompanyId = c.Id,
                            CompanyName = c.Name,
                            CompanySizeId = cz.Id,
                            CompanySizeName = cz.Name,
                            EmailAddressCompany = c.EmailAddress,
                            Phone = c.Phone,
                            AddressDetail = c.AddressDetail,
                            Website = c.Website,
                            Logo = c.Logo,
                            CreatedTime = c.CreatedTime,
                            CountJob = (from row in db.Jobs
                                        from jc in db.JobCategories
                                        from js in db.JobStatuses
                                        from jt in db.JobTypes
                                        from jp in db.JobPosition
                                        from er in db.ExperienceRange
                                        from st in db.SalaryType
                                        from e in db.Employers
                                        from com in db.Companies
                                        from rc in db.RecruitmentCampaigns
                                        where row.Active && jc.Active && js.Active && jt.Active && jp.Active && st.Active && er.Active && e.Active && com.Active && rc.Active
                                        && row.JobPositionId == jp.Id
                                        && row.JobStatusId == js.Id
                                        && row.JobTypeId == jt.Id
                                        && row.PrimaryJobCategoryId == jc.Id
                                        && row.ExperienceRangeId == er.Id
                                        && row.SalaryTypeId == st.Id
                                        && row.RecruimentCampaignId == rc.Id
                                        && com.EmployerId == rc.EmployerId
                                        && rc.EmployerId == e.Id
                                        && row.IsApproved
                                        && rc.IsAprroved && e.Id == c.EmployerId
                                        select row).Count()
                        };

           return await query.ToListAsync();
        }

        public async Task<DTResult<CandidateFollowCompanyAggregates>> PagingByCandidateId(DTPagingCandidateFollowCompanyParameters parameters)
        {
            //0.Opttion
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }

            var query = (from clc in db.CandidateFollowCompanies
                        join c in db.Companies on clc.CompanyId equals c.Id
                        join cz in db.CompanySizes on c.CompanySizeId equals cz.Id
                        where clc.CandidateId == parameters.CandidateId
                        && clc.Active
                        && c.Active
                        && cz.Active
                        select new CandidateFollowCompanyAggregates()
                        {
                            Id = clc.Id,
                            CompanyId = c.Id,
                            CompanyName = c.Name,
                            CompanySizeId = cz.Id,
                            CompanySizeName = cz.Name,
                            EmailAddressCompany = c.EmailAddress,
                            Phone = c.Phone,
                            AddressDetail = c.AddressDetail,
                            Website = c.Website,
                            Logo = c.Logo,
                            CreatedTime = c.CreatedTime,
                            CountJob = (from row in db.Jobs
                                        from jc in db.JobCategories
                                        from js in db.JobStatuses
                                        from jt in db.JobTypes
                                        from jp in db.JobPosition
                                        from er in db.ExperienceRange
                                        from st in db.SalaryType
                                        from e in db.Employers
                                        from com in db.Companies
                                        from rc in db.RecruitmentCampaigns
                                        where row.Active && jc.Active && js.Active && jt.Active && jp.Active && st.Active && er.Active && e.Active && com.Active && rc.Active
                                        && row.PrimaryJobCategoryId == jc.Id
                                        && row.JobStatusId == js.Id
                                        && row.JobTypeId == jt.Id
                                        && row.JobPositionId == jp.Id
                                        && row.ExperienceRangeId == er.Id
                                        && row.SalaryTypeId == st.Id
                                        && row.RecruimentCampaignId == rc.Id
                                        && com.EmployerId == rc.EmployerId
                                        && rc.EmployerId == e.Id
                                        && row.IsApproved
                                        && rc.IsAprroved && e.Id == c.EmployerId
                                        select row).Count()
                        });
            int recordsTotal = await query.CountAsync();

            if (!String.IsNullOrEmpty(keyword))
            {
                string noneVietnameseKeyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(e =>
                    EF.Functions.Collate(e.CompanyName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                    || EF.Functions.Collate(e.CompanySizeName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                    || e.CreatedTime.ToCustomString().Contains(noneVietnameseKeyword));
            }

            int recordsFiltered = await query.CountAsync();
            //3.Sort
            //4.Return data
            var data = await query.OrderByDescending(c => c.CreatedTime).Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            DTResult<CandidateFollowCompanyAggregates> result = new()
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
