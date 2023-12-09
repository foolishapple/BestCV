using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext context;
        public UnitOfWork(TContext _context)
        {
            context = _context;

        }
        public async Task<int> CommitAsync() => await context.SaveChangesAsync();

        public void Dispose() => context.Dispose();

    }
}
