using BestCV.Application.Models.JobServicePackages;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.Record.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class JobServicePackageService : IJobServicePackageService
    {
        private readonly IJobServicePackageRepository _jobServicePackageRepository;
        private readonly ILogger _logger;
        private readonly IEmployerServicePackageRepository _employerServicePackageRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IEmployerServicePackageEmployerRepository _employerServicePackageEmployerRepository;
        private readonly IServicePackageBenefitRepository _servicePackageBenefitRepository;
        private readonly ITopJobUrgentRepository _topJobUrgentRepository;
        private readonly ITopJobManagementRepository _topJobManagementRepository;
        private readonly ITopFeatureJobRepository _topFeatureJobRepository;
        private readonly IJobReferenceRepository _jobReferenceRepository;
        private readonly IJobSuitableRepository _jobSuitableRepository;
        private readonly IMustBeInterestedCompanyRepository _mustBeInterestedCompanyRepository;
        private readonly ITopJobExtraRepository _topJobExtraRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ITopAreaJobRepository _topAreaJobRepository;
        private readonly IRefreshJobRepository _refreshJobRepository;
        private readonly IJobRepository _jobRepository;
        public JobServicePackageService(IJobServicePackageRepository jobServicePackageRepository, ILoggerFactory loggerFactory, IEmployerServicePackageRepository employerServicePackageRepository, IEmployerRepository employerRepository, IEmployerServicePackageEmployerRepository employerServicePackageEmployerRepository, IServicePackageBenefitRepository servicePackageBenefitRepository, ITopJobUrgentRepository topJobUrgentRepository, ITopJobManagementRepository topJobManagementRepository, ITopFeatureJobRepository topFeatureJobRepository, IJobReferenceRepository jobReferenceRepository, IJobSuitableRepository jobSuitableRepository, IMustBeInterestedCompanyRepository mustBeInterestedCompanyRepository, ITopJobExtraRepository topJobExtraRepository, ICompanyRepository companyRepository, ITopAreaJobRepository topAreaJobRepository, IRefreshJobRepository refreshJobRepository, IJobRepository jobRepository)
        {
            _jobServicePackageRepository = jobServicePackageRepository;
            _logger = loggerFactory.CreateLogger<JobServicePackage>();
            _employerServicePackageRepository = employerServicePackageRepository;
            _employerRepository = employerRepository;
            _employerServicePackageEmployerRepository = employerServicePackageEmployerRepository;
            _servicePackageBenefitRepository = servicePackageBenefitRepository;
            _topJobUrgentRepository = topJobUrgentRepository;
            _topJobManagementRepository = topJobManagementRepository;
            _topFeatureJobRepository = topFeatureJobRepository;
            _jobReferenceRepository = jobReferenceRepository;
            _jobSuitableRepository = jobSuitableRepository;
            _mustBeInterestedCompanyRepository = mustBeInterestedCompanyRepository;
            _topJobExtraRepository = topJobExtraRepository;
            _companyRepository = companyRepository;
            _topAreaJobRepository = topAreaJobRepository;
            _refreshJobRepository = refreshJobRepository;
            _jobRepository = jobRepository;
        }

        public async Task<BestCVResponse> AddServiceToJob(InsertJobServicePackageDTO model)
        {
            #region Privacy Job
            if (model.EmployerId == null)
            {
                throw new Exception("employer id is null.");
            }
            else if (!await _employerRepository.PrivacyJob((long)model.EmployerId, model.JobId))
            {
                throw new Exception("Failed to privacy job.");
            }
            #endregion
            #region Check Data
            var job = await _jobRepository.GetByIdAsync(model.JobId);
            if (job == null || job.Active == false)
            {
                throw new Exception($"Failed to found job.");
            }
            var service = await _employerServicePackageRepository.GetByIdAsync(model.EmployerServicePackageId);
            if (service == null || service.Active == false)
            {
                throw new Exception($"Failed to found service package with id {model.EmployerServicePackageId}.");
            }
            List<EmployerServicePackageEmployer> eService = await _employerServicePackageEmployerRepository.FindByParams((long)model.EmployerId, model.EmployerServicePackageId, model.OrderId);
            if (eService.Count < model.Quantity)
            {
                throw new Exception($"Failed to found service package of employer.");
            }

            var benefits = await _servicePackageBenefitRepository.FindByConditionAsync(c => c.Active && c.EmployerServicePackageId == model.EmployerServicePackageId);
            if (benefits == null || benefits.Count == 0)
            {
                throw new Exception($"Failed to found service package benefit.");
            }
            #endregion
            #region Add Service
            using (var trans = await _jobServicePackageRepository.BeginTransactionAsync())
            {
                #region Delete user service 
                await _employerServicePackageEmployerRepository.SoftDeleteListAsync(eService.Take(model.Quantity).Select(c => c.Id));
                if ((await _employerServicePackageEmployerRepository.SaveChangesAsync()) >= model.Quantity)
                {
                    #region Add job service package
                    var oldService = (await _jobServicePackageRepository.FindByConditionAsync(c => c.Active && c.JobId == model.JobId && c.EmployerServicePackageId == model.EmployerServicePackageId && c.CreatedTime.AddDays(c.ExpireTime ?? 0) > DateTime.Now)).FirstOrDefault();
                    #region Apply Benefit
                    foreach (var item in benefits)
                    {
                        switch (item.BenefitId)
                        {
                            //case BenefitId.DANG_TIN_TUYEN_DUNG:
                            //    break;
                            //case BenefitId.QUA_TANG_CREDIT:
                            //    break;
                            case BenefitId.THOI_GIAN_HIEN_THI:
                                {
                                    if (oldService != null)
                                    {
                                        oldService.Quantity += model.Quantity;
                                        oldService.ExpireTime += item.Value ?? 0 * model.Quantity;
                                        await _jobServicePackageRepository.UpdateAsync(oldService);
                                    }
                                    else
                                    {
                                        await _jobServicePackageRepository.CreateAsync(new()
                                        {
                                            EmployerServicePackageId = model.EmployerServicePackageId,
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            ExpireTime = item.Value == null ? null : item.Value * model.Quantity,
                                            JobId = model.JobId,
                                            Quantity = model.Quantity
                                        });
                                    }
                                    if ((await _jobServicePackageRepository.SaveChangesAsync()) == 0)
                                    {
                                        throw new Exception("Failed to save job service package.");
                                    }
                                    break;
                                }
                            //case BenefitId.TIEP_CAN_HANG_NGAY:
                            //    break;
                            //case BenefitId.THONG_BAO_EMAIL:
                            //    {
                            //        break;
                            //    }
                            //case BenefitId.TOI_UU_BAI_DANG:
                            //    {
                            //        break;
                            //    }
                            //case BenefitId.NHAN_HO_SO_TIEU_CHUAN:
                            //    {
                            //        break;
                            //    }
                            //case BenefitId.DAY_TOP_KHUNG_GIO_VANG:
                            //    {
                            //        break;
                            //    }
                            case BenefitId.HIEN_THI_O_CONG_VIEC_NOI_BAT:
                                {
                                    if (!(await _topFeatureJobRepository.IsExisted(model.JobId)))
                                    {
                                        int maxOrderSort = await _topFeatureJobRepository.MaxOrderSort();
                                        await _topFeatureJobRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId,
                                            OrderSort = maxOrderSort + 1
                                        });
                                        await _topFeatureJobRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case BenefitId.GAN_TAG_TOP_MANAGEMENT:
                                {
                                    if (!(await _topJobManagementRepository.IsJobIdExistAsync(model.JobId)))
                                    {
                                        await _topJobManagementRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId
                                        });
                                        await _topJobManagementRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case BenefitId.HIEN_THI_DAU_TIEN_O_KET_QUA_TIM_KIEM:
                                {
                                    if (!(await _topFeatureJobRepository.IsExisted(model.JobId)))
                                    {
                                        int maxOrderSort = await _topFeatureJobRepository.MaxOrderSort();
                                        await _topFeatureJobRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId,
                                            OrderSort = maxOrderSort + 1
                                        });
                                        await _topFeatureJobRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case BenefitId.HIEN_THI_TRONG_NHOM_DAU_KET_QUA_TIM_KIEM:
                                {
                                    if (!(await _topAreaJobRepository.JobIsExisted(model.JobId)))
                                    {
                                        int maxOrderSort = await _topAreaJobRepository.LastOrderSort();
                                        await _topAreaJobRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId,
                                            OrderSort = maxOrderSort + 1,
                                            SubOrderSort = 1
                                        });
                                        await _topFeatureJobRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case BenefitId.DE_XUAT_VIEC_LAM_PHU_HOP:
                                {
                                    if (!(await _jobSuitableRepository.IsJobIdExistAsync(model.JobId)))
                                    {
                                        await _jobSuitableRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId
                                        });
                                        await _jobSuitableRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case BenefitId.DE_XUAT_VIEC_LAM_LIEN_QUAN:
                                {
                                    if (!(await _jobReferenceRepository.IsExisted(model.JobId)))
                                    {
                                        await _jobReferenceRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId
                                        });
                                        await _jobReferenceRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case BenefitId.NTD_CO_THE_BAN_QUAN_TAM:
                                {
                                    var company = await _companyRepository.FindByConditionAsync(x => x.Active && x.EmployerId == model.EmployerId);

                                    if (!(await _mustBeInterestedCompanyRepository.IsJobIdExistAsync(company[0].Id)))
                                    {
                                        await _mustBeInterestedCompanyRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            CompanyId = company[0].Id,
                                        });
                                        await _mustBeInterestedCompanyRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case BenefitId.GAN_TAG_TOP:
                                {
                                    if (!(await _topJobExtraRepository.IsExisted(model.JobId)))
                                    {
                                        int maxOrderSort = await _topJobExtraRepository.MaxOrderSort();
                                        await _topJobExtraRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId,
                                            OrderSort = maxOrderSort + 1
                                        });
                                        await _topJobExtraRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case BenefitId.GAN_TAG_URGENT:
                                {
                                    if (!(await _topJobUrgentRepository.IsExisted(model.JobId)))
                                    {
                                        int maxOrderSort = await _topJobUrgentRepository.MaxOrderSort();
                                        await _topJobUrgentRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId,
                                            OrderSort = maxOrderSort + 1
                                        });
                                        await _topJobUrgentRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                            //case BenefitId.GAN_TAG_NEW:
                            //    {
                            //        break;
                            //    }
                            case BenefitId.LAM_MOI_TIN_TUYEN_DUNG:
                                {
                                    #region add schelude refresh job job
                                    var lstRefreshJob = new List<RefreshJob>();
                                    switch (model.EmployerServicePackageId)
                                    {
                                        case EmployerServicePackageConst.Refresh_Job:
                                            {
                                                #region Refresh job
                                                job.RefreshDate = DateTime.Now;
                                                await _jobRepository.UpdateAsync(job);
                                                if (!((await _jobRepository.SaveChangesAsync()) > 0))
                                                {
                                                    throw new Exception("Failed to refresh job");
                                                }
                                                #endregion
                                            }
                                            break;
                                        case EmployerServicePackageConst.Auto_Refresh_Job_Weekly:
                                            foreach(var day in new int[]{7,14,21 })
                                            {
                                                lstRefreshJob.Add(new()
                                                {
                                                    Active = true,
                                                    CreatedTime = DateTime.Now,
                                                    JobId = model.JobId,
                                                    RefreshDate = DateTime.Now.AddDays(day)
                                                });
                                            }
                                            break;
                                        case EmployerServicePackageConst.Auto_Refresh_Job_Flexible:
                                            {
                                                #region Refresh job
                                                job.RefreshDate = DateTime.Now;
                                                await _jobRepository.UpdateAsync(job);
                                                if (!((await _jobRepository.SaveChangesAsync()) > 0))
                                                {
                                                    throw new Exception("Failed to refresh job");
                                                }
                                                #endregion
                                                for (int i = 3; i < 32; i+=3)
                                                {
                                                    lstRefreshJob.Add(new()
                                                    {
                                                        Active = true,
                                                        CreatedTime = DateTime.Now,
                                                        JobId = model.JobId,
                                                        RefreshDate = DateTime.Now.AddDays(i)
                                                    });
                                                }
                                            }
                                            break;
                                        case EmployerServicePackageConst.Auto_Refresh_Job_Daily_8_days:
                                            break;
                                        case EmployerServicePackageConst.Auto_Refresh_Job_Daily_32_days:
                                            {
                                                #region Refresh job
                                                job.RefreshDate = DateTime.Now;
                                                await _jobRepository.UpdateAsync(job);
                                                if (!((await _jobRepository.SaveChangesAsync()) > 0))
                                                {
                                                    throw new Exception("Failed to refresh job");
                                                }
                                                #endregion
                                                for (int i =1; i <32 ; i++)
                                                {
                                                    lstRefreshJob.Add(new()
                                                    {
                                                        Active = true,
                                                        CreatedTime = DateTime.Now,
                                                        JobId = model.JobId,                                                              
                                                        RefreshDate = DateTime.Now.AddDays(i)
                                                    });
                                                }
                                            }
                                            break;
                                    }
                                    if (lstRefreshJob.Count > 0)
                                    {
                                        await _refreshJobRepository.CreateListAsync(lstRefreshJob);
                                        if(!((await _refreshJobRepository.SaveChangesAsync()) > 0))
                                        {
                                            throw new Exception("Failed to add scheule refresh job");
                                        }
                                    }
                                    break;
                                    #endregion
                                }
                            //case BenefitId.HIEN_THI_O_3_NGANH_KHAC_NHAU:
                            //    {
                            //        break;
                            //    }
                            //case BenefitId.GOI_Y_TIM_KIEM:
                            //    {

                            //        break;
                            //    }
                            //case BenefitId.GOI_Y_TIM_KIEM:
                            //    {
                            //        break;
                            //    }
                            //case BenefitId.LUA_CHON_NGAY_HIEN_THI_TIN:
                            //    {
                            //        break;
                            //    }
                            //case BenefitId.BOLD_AND_RED:
                            //    {
                            //        break;
                            //    }
                            case BenefitId.TUYEN_DUNG_GAP:
                                {
                                    if (!(await _topJobUrgentRepository.IsExisted(model.JobId)))
                                    {
                                        int maxOrderSort = await _topJobUrgentRepository.MaxOrderSort();
                                        await _topJobUrgentRepository.CreateAsync(new()
                                        {
                                            Active = true,
                                            CreatedTime = DateTime.Now,
                                            JobId = model.JobId,
                                            OrderSort = maxOrderSort + 1
                                        });
                                        await _topJobUrgentRepository.SaveChangesAsync();
                                    }
                                    break;
                                }
                                //case BenefitId.HIEN_THI_DUOI_KHUNG_TIM_VIEC_TRANG_CHU:
                                //    {
                                //        break;
                                //    }
                        }
                    }
                    #endregion
                    await trans.CommitAsync();
                    return BestCVResponse.Success();
                    #endregion
                }
                #endregion
            }
            #endregion
            throw new Exception("Failed to add service pack to job");
        }

        public Task<BestCVResponse> CreateAsync(InsertJobServicePackageDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertJobServicePackageDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetByIdAsync(JobServicePackage id)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> ListAggregate(long id)
        {
            var result = await _jobServicePackageRepository.ListAggregate(id);
            return BestCVResponse.Success(result);
        }

        public Task<BestCVResponse> SoftDeleteAsync(JobServicePackage id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<JobServicePackage> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateJobServicePackageDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateJobServicePackageDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
