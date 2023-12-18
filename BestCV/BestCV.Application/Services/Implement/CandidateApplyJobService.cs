using AutoMapper;
using BestCV.Application.Models.CandidateApplyJobs;
using BestCV.Application.Models.CandidateSaveJob;
using BestCV.Application.Models.EmployerNotification;
using BestCV.Application.Services.Interfaces;
using BestCV.Application.Utilities.SignalRs.Hubs;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class CandidateApplyJobService : ICandidateApplyJobService
    {
        private readonly ICandidateApplyJobRepository _candidateApplyJobRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IEmployerNotificationRepository _employerNotificationRepository;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public CandidateApplyJobService(ICandidateApplyJobRepository candidateApplyJobRepository, ILoggerFactory loggerFactory, IMapper mapper, ICandidateRepository candidateRepository, IEmployerNotificationRepository employerNotificationRepository, IConfiguration config)
        {
            _candidateApplyJobRepository = candidateApplyJobRepository;
            _logger = loggerFactory.CreateLogger<CandidateApplyJobService>();
            _mapper = mapper;
            _candidateRepository = candidateRepository;
            _employerNotificationRepository = employerNotificationRepository;
            _config = config;
        }

        public async Task<BestCVResponse> ApplyJob(long jobId, long candidateId)
        {
            var dataAccount = await _candidateRepository.GetByIdAsync(candidateId);
            if (dataAccount == null)
            {
                return BestCVResponse.NotFound("Tài khoản không tồn tại", dataAccount);
            }
            //nếu đã tồn tại thì xóa
            if (await _candidateApplyJobRepository.IsJobIdExist(candidateId, jobId))
            {
                var applyJob = await _candidateApplyJobRepository.FindByConditionAsync(x=>x.Active && x.CandidateCVPDFId == candidateId && x.JobId == jobId && x.Id != 0);
                if (applyJob != null || applyJob.Count > 0)
                {
                    await _candidateApplyJobRepository.HardDeleteAsync(applyJob[0].Id);
                    await _candidateApplyJobRepository.SaveChangesAsync();
                    return BestCVResponse.Success(applyJob, "Deleted");
                }
            }
            //nếu chưa tồn tại thì thêm mới
            var candidateApplyJob = new CandidateApplyJobDTO
            {
                CandidateId = candidateId,
                JobId = jobId,
                IsEmployerViewed = false,
                CandidateApplyJobSourceId = 1001,
                CandidateApplyJobStatusId = 1001,
                Description = "",
                Id = 0,
                Active = true,
                CreatedTime = DateTime.Now,
               
            };
            var result = _mapper.Map<CandidateApplyJob>(candidateApplyJob);
            await _candidateApplyJobRepository.CreateAsync(result);
            await _candidateApplyJobRepository.SaveChangesAsync();
            return BestCVResponse.Success(result);
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/08/2023
        /// Description:change candidate apply job status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<BestCVResponse> ChangeStatus(ChangeStatusCandidateApplyJobDTO obj)
        {
            var item = await _candidateApplyJobRepository.GetByIdAsync(obj.Id);
            if (item == null)
            {
                throw new Exception($"Not found candidate apply job status with id: {obj.Id}");
            }
            var model = _mapper.Map(obj, item);
            await _candidateApplyJobRepository.UpdateAsync(model);
            await _candidateApplyJobRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count total cv by employer id
        /// </summary>
        /// <param name="id">employer id</param>
        /// <returns></returns>
        public async Task<BestCVResponse> CountTotalCVByEmployer(long id)
        {
            return BestCVResponse.Success(await _candidateApplyJobRepository.CountByCondition(new CountCandidateApplyJobCondition()
            {
                RecruitmentCampaginIds = new () { id }
            }));
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count total candidate cv by employer id
        /// </summary>
        /// <param name="id">employer id</param>
        /// <returns></returns>
        public async Task<BestCVResponse> CountTotalCVCandidateApplyByEmployer(long id)
        {
            return BestCVResponse.Success(await _candidateApplyJobRepository.CountByCondition(new CountCandidateApplyJobCondition()
            {
                RecruitmentCampaginIds = new () { id },
                CandidateApplyJobSourceIds = new () {CandidateApplyJobSourceConst.CANDIDATE_APPLY}
            }));
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/08/2023
        /// Updated : 22/08/2023 by ThanhNd : Check đã tồn tại
        /// Description: Create new candidate apply job
        /// </summary>
        /// <param name="obj">insert candidate apply job DTO</param>
        /// <returns>dion response</returns>
        public async Task<BestCVResponse> CreateAsync(InsertCandidateApplyJobDTO obj)
        {
            
            var item = _mapper.Map<CandidateApplyJob>(obj);
            item.Active = true;
            item.CreatedTime = DateTime.Now;
            var listErr = new List<string>();
            //nếu đã tồn tại thì trả về badrequest
            if (await _candidateApplyJobRepository.IsJobIdExist(item.CandidateCVPDFId, item.JobId))
            {
                listErr.Add("CV đã được sử dụng để ứng tuyển tin tuyển dụng này.");
                
            }
            if(listErr.Count > 0)
            {
                return BestCVResponse.BadRequest(listErr);
            }
            await _candidateApplyJobRepository.CreateAsync(item);
            await _candidateApplyJobRepository.SaveChangesAsync();
            var noti = await _candidateApplyJobRepository.DetailById(item.Id);
            try
            {
                var host = _config["SectionUrls:EmployerNotificationPage"];
                var modelnoti = new EmployerNotification
                {
                    Name = "Ứng viên "+noti.CandidateName+" đã nộp CV vào công ty",
                    Description = "Ứng viên "+noti.CandidateName+" đã nộp CV ứng tuyển vào bài " +noti.RecruimentCampaignName,
                    Link = host,
                    EmployerId = noti.EmployerId,
                    Search = "",
                    NotificationStatusId = NotificationConfig.Status.Unread,
                    NotificationTypeId = NotificationConfig.Type.Employer,
                };
                await _employerNotificationRepository.CreateAsync(modelnoti);
                await _employerNotificationRepository.SaveChangesAsync();
                var modelsendnoti = _mapper.Map<EmployerNotificationDTO>(modelnoti);
                await EmployerNotificationHub.SendNotifications(modelsendnoti);
            }
            catch (Exception ex)
            {
                _logger.LogError("Fail send notification", ex);

            }
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateApplyJobDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/08/2023
        /// Description: Datatable paging candidate apply job status
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns></returns>
        public Task<DTResult<CandidateApplyJobAggregate>> DTPaging(DTPagingCandidateApplyJobParameters parameters)
        {
            return _candidateApplyJobRepository.DTPaging(parameters);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/09/2023
        /// Description: Update employer viewed CV Status
        /// </summary>
        /// <param name="id">Mã ứng viên ứng tuyển tin tuyển dụng</param>
        /// <returns></returns>
        public async Task<BestCVResponse> EmployerViewed(long id)
        {
            var entity = await _candidateApplyJobRepository.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsEmployerViewed = true;
                await _candidateApplyJobRepository.UpdateAsync(entity);
                await _candidateApplyJobRepository.SaveChangesAsync();
                return BestCVResponse.Success();
            }
            throw new Exception($"Not found candidate apply job with id: {id}");
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DTResult<CandidateApplyJobAggregate>> PagingByCandidateId(DTPagingCandidateApplyJobParameters parameters)
        {
            return _candidateApplyJobRepository.PagingByCandidateId(parameters);
        }

        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateCandidateApplyJobDTO obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/08/2023
        /// Description:update description to candidate apply job 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<BestCVResponse> UpdateDescription(AddNoteCandidateApplyJobDTO obj)
        {
            var item = await _candidateApplyJobRepository.GetByIdAsync(obj.Id);
            if (item == null)
            {
                throw new Exception($"Not found candidate apply job status with id: {obj.Id}");
            }
            var model = _mapper.Map(obj, item);
            await _candidateApplyJobRepository.UpdateAsync(model);
            await _candidateApplyJobRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }


        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateApplyJobDTO> obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count total cv by job id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        public async Task<BestCVResponse> CountTotalToJob(long id)
        {
            return BestCVResponse.Success(await _candidateApplyJobRepository.CountByCondition(new CountCandidateApplyJobCondition()
            {
                JobIds = new() { id }
            }));
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count total cv by job id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        public async Task<BestCVResponse> CountTotalCVCandidateApplyToJob(long id)
        {
            return BestCVResponse.Success(await _candidateApplyJobRepository.CountByCondition(new CountCandidateApplyJobCondition()
            {
                JobIds = new() { id },
                CandidateApplyJobSourceIds = new() { CandidateApplyJobSourceConst.CANDIDATE_APPLY }
            }));
        }

        public async Task<BestCVResponse> GetListCandidateApplyToJob(long jobId, long candidateApplyJobId)
        {
            //var data = await _candidateApplyJobRepository.FindByConditionAsync(x=>x.Active && x.Id != candidateApplyJobId && x.JobId == jobId);
            var data = await _candidateApplyJobRepository.GetListCandidateApplyJobCompare(jobId, candidateApplyJobId);
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> DetailById(long jobId,long candidateApplyJobId)
        {
            var data = await _candidateApplyJobRepository.DetailById(jobId, candidateApplyJobId);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }
    }
}
