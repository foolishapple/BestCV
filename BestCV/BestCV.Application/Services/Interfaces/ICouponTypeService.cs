using BestCV.Application.Models.CouponType;
using BestCV.Application.Models.SalaryType;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
	public interface ICouponTypeService : IServiceQueryBase<int, InsertCouponTypeDTO, UpdateCouponTypeDTO, CouponTypeDTO>
	{
	}
}
