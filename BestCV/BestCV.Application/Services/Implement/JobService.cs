using AutoMapper;
using BestCV.Application.Models.Job;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Core.Utilities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestCV.Domain.Aggregates.Job;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc.Rendering;
using BestCV.Application.Models.CandidateApplyJobs;
using BestCV.Application.Models.WorkPlace;
using BestCV.Application.Models.JobPosition;
using BestCV.Application.Models.JobSecondaryJobPositions;
using BestCV.Application.Models.Company;
using BestCV.Application.Models.MenuType;
using BestCV.Application.Models.JobCategory;

namespace BestCV.Application.Services.Implement
{
    public class JobService : IJobService
    {

        private readonly IJobRepository jobRepository;
        private readonly IJobSecondaryJobCategoryRepository secondaryJobPositionRepository;
        private readonly IJobRequireJobSkillRepository jobRequireSkillRepository;
        private readonly IJobRequireCityRepository jobRequireCityRepository;
        private readonly IJobRequireDistrictRepository jobRequireDistrictRepository;
        private readonly IJobTagRepository jobTagRepository;
        private readonly IJobReasonApplyRepository jobReasonApplyRepository;
        private readonly ICandidateViewedJobRepository candidateViewedJobRepository;
        private readonly ILogger<IJobService> logger;
        private readonly IMapper mapper;
        public JobService(
            IJobRepository _jobRepository,
            IJobSecondaryJobCategoryRepository _secondaryJobPositionRepository,
            IJobRequireJobSkillRepository _jobRequireSkillRepository,
            IJobRequireCityRepository _jobRequireCityRepository,
            IJobRequireDistrictRepository _jobRequireDistrictRepository,
            IJobTagRepository _jobTagRepository,
            IJobReasonApplyRepository _jobReasonApplyRepository,
            ILoggerFactory loggerFactory,
            IMapper _mapper,
            ICandidateViewedJobRepository _candidateViewedJobRepository
            )
        {
            jobRepository = _jobRepository;
            jobRequireSkillRepository = _jobRequireSkillRepository;
            jobRequireCityRepository = _jobRequireCityRepository;
            jobRequireDistrictRepository = _jobRequireDistrictRepository;
            secondaryJobPositionRepository = _secondaryJobPositionRepository;
            jobReasonApplyRepository = _jobReasonApplyRepository;
            jobTagRepository = _jobTagRepository;
            logger = loggerFactory.CreateLogger<JobService>();
            mapper = _mapper;
            candidateViewedJobRepository = _candidateViewedJobRepository;
        }


        public async Task<BestCVResponse> CreateAsync(InsertJobDTO obj)
        {

            using (var database = await jobRepository.BeginTransactionAsync())
            {
                try
                {
                    // lưu vào bảng Job
                    var job = mapper.Map<Job>(obj);

                    job.JobStatusId = obj.JobStatusId == 1 ? JobStatusConst.STATUS_PUBLISHED : JobStatusConst.STATUS_DRAFT;
                    job.Search = $"{obj.Name.RemoveVietnamese()} {(obj.Overview != "" ? obj.Overview.RemoveVietnamese() : "")} {obj.Requirement.RemoveVietnamese()} {obj.Benefit.RemoveVietnamese()} {(obj.ReceiverName != "" ? obj.ReceiverName.RemoveVietnamese() : "")} {obj.ReceiverPhone} {obj.ReceiverEmail}  {obj.ApplyEndDate}";

                    await jobRepository.CreateAsync(job);
                    await jobRepository.SaveChangesAsync();

                    if (job.Id > 0)
                    {
                        // lưu vào bảng JobRequireSkill
                        #region JobRequireSkill
                        var listRequireSkillDTO = new List<JobRequireJobSkill>();
                        foreach (var id in obj.ListJobRequireSkill)
                        {
                            var requireItemDTO = new JobRequireJobSkill()
                            {
                                JobId = job.Id,
                                JobSkillId = id,
                            };
                            listRequireSkillDTO.Add(requireItemDTO);
                        }
                        await jobRequireSkillRepository.CreateListAsync(listRequireSkillDTO);
                        var countJRS = await jobRequireSkillRepository.SaveChangesAsync();
                        if (countJRS != listRequireSkillDTO.Count)
                        {
                            await jobRepository.RollbackTransactionAsync();
                            logger.LogError("ROLLBACK_TRANSACTION: Thêm jobRequireSkill không thành công khi tạo job");
                            return BestCVResponse.Error("Thêm kỹ năng yêu cầu không thành công khi tạo tin tuyển dụng");
                        }
                        #endregion

                        // lưu vào bảng JobSecondaryPosition
                        #region JobSecondaryPosition
                        var listJobSecondaryPositionDTO = new List<JobSecondaryJobCategory>();
                        if (obj.ListJobSecondaryJobCategory.Count > 0)
                        {
                            foreach (var id in obj.ListJobSecondaryJobCategory)
                            {
                                var requireItemDTO = new JobSecondaryJobCategory()
                                {
                                    JobId = job.Id,
                                    JobCategoryId = id
                                };
                                listJobSecondaryPositionDTO.Add(requireItemDTO);
                            }
                            await secondaryJobPositionRepository.CreateListAsync(listJobSecondaryPositionDTO);
                            var countJSP = await secondaryJobPositionRepository.SaveChangesAsync();
                            if (countJSP != listJobSecondaryPositionDTO.Count)
                            {
                                await jobRepository.RollbackTransactionAsync();
                                logger.LogError("ROLLBACK_TRANSACTION: Thêm JobSecondaryPosition không thành công khi tạo job");
                                return BestCVResponse.Error("Thêm ngành nghề phụ không thành công khi tạo tin tuyển dụng");
                            }
                        }
                        #endregion

                        // lưu vào bảng JobReasonApply
                        #region JobReasonApply
                        var listReasonApplyDTO = new List<JobReasonApply>();
                        if (obj.ListJobReasonApply.Count > 0)
                        {
                            foreach (var item in obj.ListJobReasonApply)
                            {
                                var reason = new JobReasonApply()
                                {
                                    JobId = job.Id,
                                    Name = item,
                                    Search = item,
                                };
                                listReasonApplyDTO.Add(reason);
                            }
                            await jobReasonApplyRepository.CreateListAsync(listReasonApplyDTO);
                            var countJRA = await jobReasonApplyRepository.SaveChangesAsync();
                            if (countJRA != listReasonApplyDTO.Count)
                            {
                                await jobRepository.RollbackTransactionAsync();
                                logger.LogError("ROLLBACK_TRANSACTION: Thêm JobReasonApply không thành công khi tạo job");
                                return BestCVResponse.Error("Thêm quyền lợi nổi bật không thành công khi tạo tin tuyển dụng");
                            }
                        }
                        #endregion

                        // lưu vào bảng
                        #region JobTag
                        var listTagDTO = new List<JobTag>();
                        if (obj.ListTag.Count > 0)
                        {
                            foreach (var id in obj.ListTag)
                            {
                                var tagItem = new JobTag()
                                {
                                    JobId = job.Id,
                                    TagId = id,
                                };
                                listTagDTO.Add(tagItem);
                            }
                            await jobTagRepository.CreateListAsync(listTagDTO);
                            var countJT = await jobTagRepository.SaveChangesAsync();
                            if (countJT != listTagDTO.Count)
                            {
                                await jobRepository.RollbackTransactionAsync();
                                logger.LogError("ROLLBACK_TRANSACTION: Thêm JobTag không thành công khi tạo job");
                                return BestCVResponse.Error("Thêm hashtag không thành công khi tạo tin tuyển dụng");
                            }
                        }
                        #endregion


                        // bảng city & bảng district
                        #region JobCity

                        List<int> listInsertingCityId = new();
                        for (int i = 0; i < obj.ListJobRequireWorkplace.Count; i++)
                        {
                            var workplace = obj.ListJobRequireWorkplace[i];

                            // mapping sang enity để add vào bảng
                            var requireCityDTO = mapper.Map<JobRequireCity>(workplace);
                            requireCityDTO.JobId = job.Id;
                            requireCityDTO.Search = "";

                            // vòng lặp đầu tiên thêm vào bình thường
                            if (i == 0)
                            {
                                // lưu vào bảng jobRequireCity (vì khóa ngoại bảng JobRequireDistrict cần đến Id bảng JobRequireCity)
                                await jobRequireCityRepository.CreateAsync(requireCityDTO);
                                await jobRequireCityRepository.SaveChangesAsync();
                                listInsertingCityId.Add(requireCityDTO.CityId);

                                // nếu có districtId
                                if (workplace.DistrictId != null)
                                {
                                    var requireDistrictDTO = mapper.Map<JobRequireDistrict>(workplace);
                                    requireDistrictDTO.Search = workplace.AddressDetail + "";
                                    requireDistrictDTO.JobRequireCityId = requireCityDTO.Id;

                                    // lưu vào bảng JobRequireDistrict
                                    await jobRequireDistrictRepository.CreateAsync(requireDistrictDTO);
                                    await jobRequireDistrictRepository.SaveChangesAsync();

                                }
                            }
                            // bắt đầu so sánh từ vòng lặp thứ 2
                            else
                            {
                                // so sánh nếu cityId nhận vào nằm trong mảng distinct thì ko tạo mới nữa mà thêm district dựa trên cityId đó luôn
                                // và districtId not null
                                if (listInsertingCityId.Contains(workplace.CityId) && workplace.DistrictId != null)
                                {
                                    // tìm id của row trong bảng JobRequireCity có cityId trùng với "workplace.CityId"
                                    var jobRequireCityId = await jobRequireCityRepository.FindByCondition(x => x.CityId.Equals(workplace.CityId) && x.JobId.Equals(job.Id) && x.Active)
                                        .Select(x => x.Id)
                                        .FirstOrDefaultAsync();
                                    if (jobRequireCityId != 0)
                                    {
                                        var requireDistrictDTO = mapper.Map<JobRequireDistrict>(workplace);
                                        requireDistrictDTO.Search = workplace.AddressDetail + "";
                                        requireDistrictDTO.JobRequireCityId = jobRequireCityId;

                                        // lưu vào bảng JobRequireDistrict
                                        await jobRequireDistrictRepository.CreateAsync(requireDistrictDTO);
                                        await jobRequireDistrictRepository.SaveChangesAsync();
                                    }

                                }
                                // so sánh nếu cityId nhận vào nằm trong mảng distinct thì ko tạo mới nữa
                                // và districtId not null
                                else
                                {
                                    // lưu vào bảng jobRequireCity (vì khóa ngoại bảng JobRequireDistrict cần đến Id bảng JobRequireCity)
                                    await jobRequireCityRepository.CreateAsync(requireCityDTO);
                                    await jobRequireCityRepository.SaveChangesAsync();
                                    listInsertingCityId.Add(requireCityDTO.CityId);

                                    // nếu ko tạo được thì rollback
                                    if (requireCityDTO.Id <= 0)
                                    {
                                        await jobRepository.RollbackTransactionAsync();
                                        logger.LogError("ROLLBACK_TRANSACTION: Thêm jobRequireCity không thành công khi tạo job");
                                        return BestCVResponse.Error("Thêm khu vực làm việc không thành công khi tạo tin tuyển dụng");
                                    }
                                    // tạo thành công thì thực hiện tạo district & address nếu có
                                    else
                                    {
                                        #region JobDistrict

                                        // mapping model
                                        if (workplace.DistrictId != null)
                                        {
                                            var requireDistrictDTO = mapper.Map<JobRequireDistrict>(workplace);
                                            requireDistrictDTO.Search = workplace.AddressDetail + "";
                                            requireDistrictDTO.JobRequireCityId = requireCityDTO.Id;

                                            // lưu vào bảng JobRequireDistrict
                                            await jobRequireDistrictRepository.CreateAsync(requireDistrictDTO);
                                            await jobRequireDistrictRepository.SaveChangesAsync();

                                            // nếu ko tạo được thì rollback
                                            if (requireDistrictDTO.Id <= 0)
                                            {
                                                await jobRepository.RollbackTransactionAsync();
                                                logger.LogError("ROLLBACK_TRANSACTION: Thêm jobRequireDistrict không thành công khi tạo job");
                                                return BestCVResponse.Error("Thêm quận/huyện, địa chỉ làm việc không thành công khi tạo tin tuyển dụng");
                                            }
                                        }

                                        #endregion
                                    }
                                }
                            }

                        }

                        #endregion

                        await jobRepository.EndTransactionAsync();
                        return BestCVResponse.Success(job);
                    }
                    else
                    {
                        await jobRepository.RollbackTransactionAsync();
                        return BestCVResponse.Error();
                    }
                }
                catch (Exception ex)
                {
                    await jobRepository.RollbackTransactionAsync();
                    logger.LogError(ex, $"Có lỗi khi tạo tin tuyển dụng {obj}");
                    return BestCVResponse.BadRequest(ex);
                }
            }

        }
        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertJobDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await jobRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }

            var res = mapper.Map<List<JobDTO>>(data);
            return BestCVResponse.Success(res);
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await jobRepository.GetByIdAsync(id);
            if (data != null)
            {
                return BestCVResponse.Success(data);
            }
            return BestCVResponse.BadRequest(id);
        }

        public async Task<List<SelectListItem>> ListJobCategorySelected()
        {
            return await jobRepository.ListJobCategorySelected();
        }


        public async Task<List<SelectListItem>> ListJobTypeSelected()
        {
            return await jobRepository.ListJobTypeSelected();
        }


        public async Task<List<SelectListItem>> ListJobStatusSelected()
        {
            return await jobRepository.ListJobStatusSelected();
        }


        public async Task<List<SelectListItem>> ListJobExperienceSelected()
        {
            return await jobRepository.ListJobExperienceSelected();
        }


        public async Task<List<SelectListItem>> ListCampaignSelected()
        {
            return await jobRepository.ListCampaignSelected();
        }


        public async Task<object> ListRecruitmentNewsAggregates(DTParameters parameters)
        {
            return await jobRepository.ListRecruitmentNewsAggregates(parameters);
        }
        public async Task<BestCVResponse> LoadDataFilterJobHomePageAsync()
        {
            var data = await jobRepository.LoadDataFilterJobHomePageAsync();
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);

        }
        public async Task<BestCVResponse> SearchJobHomePageAsync(SearchingJobParameters parameter)
        {
            var data = await jobRepository.SearchJobHomePageAsync(parameter);
            if (data.DataSource == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateJobDTO obj)
        {
            using (var database = await jobRepository.BeginTransactionAsync())
            {
                try
                {
                    var oldJob = await jobRepository.GetDetailById(obj.Id);
                    var jobEntity = mapper.Map<Job>(oldJob);
                    if (oldJob != null)
                    {
                        #region Job
                        var updatingJob = mapper.Map(obj, jobEntity);

                        updatingJob.JobStatusId = obj.JobStatusId == 1 ? JobStatusConst.STATUS_PUBLISHED : JobStatusConst.STATUS_DRAFT;
                        updatingJob.Search = $"{obj.Name.RemoveVietnamese()} {(obj.Overview != "" ? obj.Overview.RemoveVietnamese() : "")} {obj.Requirement.RemoveVietnamese()} {obj.Benefit.RemoveVietnamese()} {(obj.ReceiverName != "" ? obj.ReceiverName.RemoveVietnamese() : "")} {obj.ReceiverPhone} {obj.ReceiverEmail}  {obj.ApplyEndDate}";

                        // lưu update vào bảng job
                        await jobRepository.UpdateAsync(updatingJob);
                        await jobRepository.SaveChangesAsync();
                        #endregion

                        #region JobTag

                        // tìm các tagId có sự thay đổi
                        var removedListTag = oldJob.ListTag.Except(obj.ListTag).ToList();
                        var addedListTag = obj.ListTag.Except(oldJob.ListTag).ToList();
                        // chuyển đổi sang mảng mới để xóa bằng ID
                        if (removedListTag.Count > 0)
                        {
                            var listIdRemove = new List<long>();
                            listIdRemove = (await jobTagRepository.FindByConditionAsync(x => x.JobId.Equals(oldJob.Id) && removedListTag.Contains(x.TagId))).Select(c => c.Id).ToList();
                            await jobTagRepository.SoftDeleteListAsync(listIdRemove);
                            await jobTagRepository.SaveChangesAsync();
                        }
                        // chuyển sang mảng mới để add
                        if (addedListTag.Count > 0)
                        {
                            var listIdAdd = new List<JobTag>();
                            foreach (var item in addedListTag)
                            {
                                var subData = new JobTag()
                                {
                                    TagId = item,
                                    JobId = oldJob.Id,
                                };
                                listIdAdd.Add(subData);
                            }
                            // thực hiện thêm trong bảng jobTag
                            await jobTagRepository.CreateListAsync(listIdAdd);
                            await jobTagRepository.SaveChangesAsync();

                        }
                        #endregion

                        #region JobRequireSkill

                        // tìm các SkillId có sự thay đổi
                        var removedListSkill = oldJob.ListJobRequireSkill.Except(obj.ListJobRequireSkill).ToList();
                        var addedListSkill = obj.ListJobRequireSkill.Except(oldJob.ListJobRequireSkill).ToList();
                        // chuyển đổi sang mảng Object mới để xóa bằng ID
                        if (removedListSkill.Count > 0)
                        {
                            var listIdRemove = new List<long>();
                            listIdRemove = (await jobRequireSkillRepository.FindByConditionAsync(x => x.JobId.Equals(oldJob.Id) && removedListSkill.Contains(x.JobSkillId))).Select(c => c.Id).ToList();

                            // thực hiện xóa trong bảng jobSkill
                            await jobRequireSkillRepository.SoftDeleteListAsync(listIdRemove);
                            await jobRequireSkillRepository.SaveChangesAsync();

                        }
                        // chuyển sang mảng Object mới để add
                        if (addedListSkill.Count > 0)
                        {
                            var listIdAdd = new List<JobRequireJobSkill>();
                            foreach (var item in addedListSkill)
                            {
                                var subData = new JobRequireJobSkill()
                                {
                                    JobSkillId = item,
                                    JobId = oldJob.Id,
                                };
                                listIdAdd.Add(subData);
                            }
                            // thực hiện thêm trong bảng jobSkill
                            await jobRequireSkillRepository.CreateListAsync(listIdAdd);
                            await jobRequireSkillRepository.SaveChangesAsync();

                        }

                        #endregion

                        #region JobSecondaryPosition

                        // tìm các secondaryCategoryId có sự thay đổi
                        var removedListSecondaryCategory = oldJob.ListJobSecondaryJobCategory.Except(obj.ListJobSecondaryJobCategory).ToList();
                        var addedListSecondaryPosition = obj.ListJobSecondaryJobCategory.Except(oldJob.ListJobSecondaryJobCategory).ToList();
                        // chuyển đổi sang mảng Object mới để xóa bằng ID
                        if (removedListSecondaryCategory.Count > 0)
                        {
                            var listIdRemove = new List<long>();
                            listIdRemove = (await secondaryJobPositionRepository.FindByConditionAsync(x => x.JobId.Equals(oldJob.Id) && removedListTag.Contains(x.JobCategoryId))).Select(c => c.Id).ToList();

                            // thực hiện xóa trong bảng jobTag
                            await secondaryJobPositionRepository.SoftDeleteListAsync(listIdRemove);
                            await secondaryJobPositionRepository.SaveChangesAsync();

                        }
                        // chuyển sang mảng Object mới để add
                        if (addedListSecondaryPosition.Count > 0)
                        {
                            var listIdAdd = new List<JobSecondaryJobCategory>();
                            foreach (var item in addedListSecondaryPosition)
                            {
                                var subData = new JobSecondaryJobCategory()
                                {
                                    JobCategoryId = item,
                                    JobId = oldJob.Id,
                                };
                                listIdAdd.Add(subData);
                            }
                            // thực hiện thêm trong bảng jobTag
                            await secondaryJobPositionRepository.CreateListAsync(listIdAdd);
                            await secondaryJobPositionRepository.SaveChangesAsync();

                        }

                        #endregion

                        #region JobReasonApply
                        var listJobReasonUpdate = new List<JobReasonApply>();
                        for (int i = 0; i < obj.ListJobReasonApply.Count; i++)
                        {
                            oldJob.ListJobReasonApply[i].Name = obj.ListJobReasonApply[i].Name;
                            //var updatingReason = mapper.Map<JobReasonApply>(oldJob.ListJobReasonApply[i]);
                            listJobReasonUpdate.Add(oldJob.ListJobReasonApply[i]);
                            // thực hiện lưu vào bảng JobReasonApply
                            await jobReasonApplyRepository.UpdateListAsync(listJobReasonUpdate);
                            await jobReasonApplyRepository.SaveChangesAsync();
                        }
                        #endregion

                        #region JobWorkPlace


                        #endregion

                        await database.CommitAsync();
                        return BestCVResponse.Success(updatingJob);
                    }
                    await database.RollbackAsync();
                    return BestCVResponse.NotFound("Không tìm thấy tin tuyển dụng cần cập nhật", obj);
                }
                catch (Exception e)
                {
                    await database.RollbackAsync();
                    logger.LogError(e, $"Có lỗi khi tạo tin tuyển dụng {obj}");
                    return BestCVResponse.BadRequest(e);
                }
            }
        }
        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateJobDTO> obj)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> QuickIsApprovedAsync(long id)
        {
            var isUpdated = await jobRepository.QuickIsApprovedAsync(id);
            if (isUpdated)
            {
                await jobRepository.SaveChangesAsync();
                return BestCVResponse.Success(isUpdated);
            }
            return BestCVResponse.BadRequest("Kích hoạt tin tuyển dụng không thành công");
        }

        public async Task<BestCVResponse> AdminDetailAsync(long id)
        {
            var data = await jobRepository.GetByIdAsync(id);
            if (data != null)
            {
                var model = mapper.Map<JobDetailDTO>(data);
                var recruitmentCampaign = await jobRepository.GetRecruitmentCampaignByJobIdAsync(id);
                if (recruitmentCampaign != null)
                {
                    model.RecruimentCampaignName = recruitmentCampaign.Name;
                    var company = await jobRepository.GetCompanyByRecruitmentCampaignIdAsync(recruitmentCampaign.Id);
                    if (company != null)
                    {
                        model.Company.Add(new CompanyDTO
                        {
                            id = company.Id,
                            Name = company.Name,
                            Website = company.Website,
                        });
                    }
                }
                //jobrequirejobskill
                model.ListJobRequireJobSkill = await jobRepository.GetJobSkillIdsByJobIdAsync(id);

                // Định dạng lương
                if (data.SalaryFrom.HasValue && !data.SalaryTo.HasValue)
                {
                    model.FormattedSalary = $"Từ {data.SalaryFrom.Value:N0}"; // Trường hợp 1
                }
                else if (data.SalaryFrom.HasValue && data.SalaryTo.HasValue)
                {
                    model.FormattedSalary = $"{data.SalaryFrom.Value:N0} - {data.SalaryTo.Value:N0}"; // Trường hợp 2
                }
                else if (!data.SalaryFrom.HasValue && data.SalaryTo.HasValue)
                {
                    model.FormattedSalary = $"Đến {data.SalaryTo.Value:N0}"; // Trường hợp 3
                }

                //Lấy danh sách nơi làm việc liên quan đến công việc
                var jobWorkPlaces = await jobRepository.GetWorkPlacesByJobIdAsync(id);
                model.ListJobRequireWorkplace = jobWorkPlaces.Select(wp => new JobWorkPlaceDTO
                {
                    CityId = wp.Id,
                    CityName = wp.Name,
                }).ToList();

                //Gọi trực tiếp từ repo và chuyển đổi kết quả

                var jobdetail = await jobRepository.GetJobDetailByJobIdAsync(id);
                // Lấy danh sách ngành nghề liên quan
                var secondaryJobCategories = await jobRepository.GetSecondaryJobCategoriesByJobIdAsync(id);
                model.ListJobSecondaryCategories = secondaryJobCategories
                    .Select(p => p.JobCategory.Id)
                    .ToList();

                // Lấy danh sách mã thẻ
                var tags = await jobRepository.GetTagIdsByJobIdAsync(id);
                model.ListTag = tags.ToList();


                var candidateApplyJobs = await jobRepository.GetCandidateApplyJobByJobIdAsync(id);
                model.ListCvs = candidateApplyJobs.Select(candidateApplyJob => new CandidateApplyJobDTO
                {
                    Id = candidateApplyJob.Id,
                    // Ánh xạ các trường khác ở đây
                    CandidateApplyJobStatusName = candidateApplyJob.CandidateApplyJobStatusName,
                    CandidateId = candidateApplyJob.CandidateId,
                    CandidateCvPdfUrl = candidateApplyJob.CandidateCVPDFUrl,
                    CandidateName = candidateApplyJob.CandidateName,
                    CandidateApplyJobStatusColor = candidateApplyJob.CandidateApplyJobStatusColor,
                    IsEmployerViewed = candidateApplyJob.IsEmployerViewed,
                    IsEmployerViewedDisplay = candidateApplyJob.IsEmployerViewed ? "Đã đọc" : "Chưa đọc",
                    CreatedTime = candidateApplyJob.CreatedTime,
                }).ToList();


                return BestCVResponse.Success(model);
            }
            return BestCVResponse.NotFound("Không tìm thấy tin tuyển dụng", data);
        }

        public async Task<BestCVResponse> GetDetailJobOnHomePageAsync(long jobId, long candidateId)
        {
            var data = await jobRepository.GetDetailJobAsync(jobId, candidateId);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }


        public async Task AddToListViewed(long jobId, long candidateId)
        {
            var data = await candidateViewedJobRepository.FindByConditionAsync(x => x.Active && x.JobId == jobId && x.CandidateId == candidateId);
            if (data.Count == 0 || data == null)
            {
                var candidate = new CandidateViewedJob
                {
                    CandidateId = candidateId,
                    JobId = jobId,
                };
                await candidateViewedJobRepository.CreateAsync(candidate);
                await candidateViewedJobRepository.SaveChangesAsync();
            }
        }

        public async Task<BestCVResponse> ListJobReference(int categoryId, int typeId)
        {
            return BestCVResponse.Success(await jobRepository.ListJobReference(categoryId, typeId));
        }

        public async Task<BestCVResponse> ListByRecruitCampain(long id)
        {
            var data = await jobRepository.ListByRecruitCampain(id);
            return BestCVResponse.Success(data);
        }


        public async Task<DTResult<EmployerJobAggregate>> ListDTPaging(DTJobPagingParameters parameters)
        {
            return await jobRepository.ListDTPaging(parameters);
        }


        public async Task<BestCVResponse> ListJobByCompanyId(int companyId, long candidateId, int quantity)
        {
            var data = await jobRepository.ListJobByCompanyId(companyId, candidateId, quantity);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> CountByCondition(CountJobCondition condition)
        {
            var total = await jobRepository.CountByCondition(condition);
            return BestCVResponse.Success(total);
        }

        public async Task<BestCVResponse> GetDetalById(long jobId)
        {
            var data = await jobRepository.GetDetailById(jobId);
            if (data != null)
            {
                return BestCVResponse.Success(data);
            }
            logger.LogError($"Failed to get detail of jobId [{jobId}]");
            return BestCVResponse.NotFound("Không tìm thấy tin tuyển dụng", jobId);
        }


        public async Task<IEnumerable<JobCategoryDTO>> GetAllPrimaryJobCategoryNamesAsync()
        {
            var jobs = await jobRepository.GetAllPrimaryJobCategoriesAsync();
            return jobs.Select(j => new JobCategoryDTO
            {
                Id = j.PrimaryJobCategoryId,
                Name = j.PrimaryJobCategory.Name
            }).Distinct();
        }

        public async Task<List<JobCategory>> GetSecondaryJobCategoriesForSelect()
        {
            return await jobRepository.GetSecondaryJobCategoriesForSelect();
        }


        public async Task<List<Tag>> GetJobTagsAsync()
        {
            return await jobRepository.GetJobTagsAsync();
        }

        public async Task<List<JobSkill>> GetJobSkillsAsync()
        {
            return await jobRepository.GetJobSkillsAsync();
        }

        public async Task<BestCVResponse> Privacy(long id, long userId)
        {
            var result = await jobRepository.Privacy(id, userId);
            if (result)
            {
                return BestCVResponse.Success();
            }
            return BestCVResponse.Error($"Failed to privacy with jobId {id} and userId {userId}");
        }

        public async Task AddViewCount(long id)
        {
            var job = await jobRepository.GetByIdAsync(id);
            if (job != null)
            {
                job.ViewCount++;
                await jobRepository.UpdateAsync(job);
                await jobRepository.SaveChangesAsync();
            }
            throw new Exception($"Failed to found job by id: {id}");
        }

        public async Task<BestCVResponse> ListSuggestion()
        {
            var result = await jobRepository.ListSuggestion();
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SearchSuggestion(string keyword)
        {
            var result = await jobRepository.SearchSuggestion(keyword);
            return BestCVResponse.Success(result);
        }
    }
}
