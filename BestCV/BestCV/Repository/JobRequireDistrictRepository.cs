using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class JobRequireDistrictRepository : RepositoryBaseAsync<JobRequireDistrict, long, JobiContext>, IJobRequireDistrictRepository
    {
        private readonly JobiContext _db;
        private IUnitOfWork<JobiContext> _unitOfWork;
        public JobRequireDistrictRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
    }
}
