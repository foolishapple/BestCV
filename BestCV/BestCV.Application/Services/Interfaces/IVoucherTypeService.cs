using BestCV.Application.Models.VoucherTypes;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IVoucherTypeService : IServiceQueryBase<int, InsertVoucherTypeDTO, UpdateVoucherTypeDTO, VoucherTypeDTO>
    {
    }
}
