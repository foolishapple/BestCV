using BestCV.Application.Models.CompanySize;
using BestCV.Application.Models.CouponType;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICompanySizeService : IServiceQueryBase<int, InsertCompanySizeDTO, UpdateCompanySizeDTO, CompanySizeDTO>
    {
        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 17/08/2023
        /// Description : lấy ra thông tin của quy mô công ty và số lượng công ty thuộc quy mô này
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public Task<DionResponse> LoadDataFilterCompanySizeHomePageAsync();
    }
}
