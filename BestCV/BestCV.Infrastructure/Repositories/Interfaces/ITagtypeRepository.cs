using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ITagtypeRepository : IRepositoryBaseAsync<TagType , int ,  JobiContext>
    {
        Task<bool> IsTagTypeExistAsync(string name, int id);
    }
}
