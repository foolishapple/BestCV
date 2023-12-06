using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.EmployerServicePackageEmployers;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class EmployerServicePackageEmployerRepository : RepositoryBaseAsync<EmployerServicePackageEmployer, long, JobiContext>, IEmployerServicePackageEmployerRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public EmployerServicePackageEmployerRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/09/2023
        /// Description: Check service package added in order
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        public async Task<bool> CheckServiceAdded(long id)
        {
            var query = from es in _db.EmployerServicePackageEmployers
                        join od in _db.EmployerOrderDetails on es.EmployerOrderDetailId equals od.Id
                        join o in _db.EmployerOrders on od.OrderId equals o.Id
                        where es.Active && od.Active && o.Active && o.Id == id
                        select es;
            var result = await query.AnyAsync();
            return result;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 18/09/2023
        /// Description: find employer service package id 
        /// </summary>
        /// <param name="employerId">mã nhà tuyển dụng</param>
        /// <param name="servicePackageId">mã gói dịch vụ</param>
        /// <returns></returns>
        public async Task<List<EmployerServicePackageEmployer>> FindByParams(long employerId, long servicePackageId, long orderId)
        {
            var query = from esp in _db.EmployerServicePackageEmployers
                        join od in _db.EmployerOrderDetails on esp.EmployerOrderDetailId equals od.Id
                        join o in _db.EmployerOrders on od.OrderId equals o.Id
                        join e in _db.Employers on o.EmployerId equals e.Id
                        join sp in _db.EmployerServicePackages on od.EmployerServicePackageId equals sp.Id
                        where esp.Active && od.Active && o.Active && e.Active && sp.Active && e.Id == employerId && servicePackageId == sp.Id && o.Id == orderId
                        select esp;
            var result = await query.ToListAsync();
            return result;
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 17/09/2023
        /// Description: Paging group by parameters
        /// </summary>
        /// <param name="parameters">paging parameters</param>
        /// <returns></returns>
        public async Task<List<EmployerServicePackageEmployerGroupAggregate>> GroupByParameters(DTEmployerServicePackageEmployerParameters parameters)
        {
            //0.Query
            var query = from esp in (from esp in _db.EmployerServicePackageEmployers
                                     where esp.Active
                                     group esp.Id by esp.EmployerOrderDetailId into gesp
                                     select new { key = gesp.Key, quantity = gesp.Count() })
                        join od in _db.EmployerOrderDetails on esp.key equals od.Id
                        join o in _db.EmployerOrders on od.OrderId equals o.Id
                        join e in _db.Employers on o.EmployerId equals e.Id
                        join sp in _db.EmployerServicePackages on od.EmployerServicePackageId equals sp.Id
                        join spt in _db.ServicePackageTypes on sp.ServicePackageTypeId equals spt.Id
                        join spg in _db.ServicePackageGroups on sp.ServicePackageGroupId equals spg.Id
                        where od.Active && o.Active && e.Active && sp.Active && spt.Active && spg.Active
                        select new EmployerServicePackageEmployerGroupAggregate()
                        {
                            ServicePackageId = sp.Id,
                            ServicePackageTypeId = spt.Id,
                            ServicePackageName = sp.Name,
                            ServicePackageTypeName = spt.Name,
                            EmployerId = e.Id,
                            Quantity = esp.quantity,
                            OrderId = o.Id,
                            ServicePackageGroupId = spg.Id,
                            ServicePackageGroupName = spg.Name
                        };
            //1.Filter
            if (parameters.EmployerId != null)
            {
                query = query.Where(c => c.EmployerId == parameters.EmployerId);
            }
            if(parameters.EmployerServicePackageTypeId.Length>0 )
            {
                query = query.Where(c => parameters.EmployerServicePackageTypeId.Contains(c.ServicePackageTypeId));
            }
            //2.Return data
            var result = await query.ToListAsync();
            return result;
        }
    }
}
