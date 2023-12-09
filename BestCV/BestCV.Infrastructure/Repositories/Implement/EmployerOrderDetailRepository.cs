using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class EmployerOrderDetailRepository : RepositoryBaseAsync<EmployerOrderDetail, long, JobiContext>, IEmployerOrderDetailRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public EmployerOrderDetailRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }
    }
}
