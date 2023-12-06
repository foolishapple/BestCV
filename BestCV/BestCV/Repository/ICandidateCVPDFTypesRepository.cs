using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateCVPDFTypesRepository: IRepositoryBaseAsync<CandidateCVPDFType, int, JobiContext>
    {
        Task<bool> IsExistAsync(string name, int id);
    }
}
