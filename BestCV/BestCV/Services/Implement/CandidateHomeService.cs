using Jobi.Application.Services.Interfaces;
using Jobi.Core.Entities;
using Jobi.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Application.Services.Implement
{
    public class CandidateHomeService : ICandidateHomeService
    {
        private readonly ITopCompanyRepository _topCompanyRepository;
        private readonly ITopFeatureJobRepository _topJobRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IJobCategoryRepository _jobCategoryRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICandidateRepository _candidateRepository;
        public CandidateHomeService(ITopCompanyRepository topCompanyRepository, ITopFeatureJobRepository topJobRepository, IJobRepository jobRepository, IJobCategoryRepository jobCategoryRepository, IPostRepository postRepository, ICompanyRepository companyRepository, ICandidateRepository candidateRepository)
        {
            _topCompanyRepository = topCompanyRepository;
            _topJobRepository = topJobRepository;
            _jobRepository = jobRepository;
            _jobCategoryRepository = jobCategoryRepository;
            _postRepository = postRepository;
            _companyRepository = companyRepository;
            _candidateRepository = candidateRepository;
        }

        public int CountJob()
        {
            var data = _jobRepository.CountJob();
            return data;
        }

        public async Task<DionResponse> GetListJobCategoryShowonHomepage()
        {
            var data = await _jobCategoryRepository.ListCategoryOnHomepage();
            if(data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);

            }
            return DionResponse.Success(data);
        }

        //public async Task<DionResponse> GetDetailJobAsync(long jobId)
        //{
        //    var data = await _jobRepository.GetDetailJobAsync(jobId);
        //    if (data == null)
        //    {
        //        return DionResponse.NotFound("Không có dữ liệu", data);
        //    }
        //    return DionResponse.Success(data);
        //}

        public async Task<DionResponse> GetListTopCompanyShowonHomepage()
        {
            var data = await _topCompanyRepository.ListTopCompanyShowOnHomePageAsync();
            if(data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> GetListTopJobShowonHomepage()
        {
            var data = await _topJobRepository.ListTopFeatureJobShowOnHomePageAsync();
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> GetListTopPostShowonHomepage()
        {
            var data = await _postRepository.ListPostShowonHomepage();
            if( data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/09/2023
        /// Description: Check job is actived
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> JobIsActive(long id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if(job !=null && job.Active)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/09/2023
        /// Description: Check candidate is actived
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CandidateIsActive(long id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate != null && candidate.Active)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/09/2023
        /// Description: Check company is actived
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CompanyIsActive(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company != null && company.Active)
            {
                return true;
            }
            return false;
        }
    }
}
