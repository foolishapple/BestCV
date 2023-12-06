using Microsoft.EntityFrameworkCore;
using Jobi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobi.Domain.Constants;

namespace Jobi.Infrastructure.Persistence
{
    public static class JobiContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            #region Account_Status
            modelBuilder.Entity<AccountStatus>().HasData(new AccountStatus()
            {
                Id = 1001,
                Active = true,
                Color = "green",
                CreatedTime = DateTime.Now,
                Name = "Active"
            });
            modelBuilder.Entity<AccountStatus>().HasData(new AccountStatus()
            {
                Id = 1002,
                Active = true,
                Color = "red",
                CreatedTime = DateTime.Now,
                Name = "Block"
            });
            #endregion

            #region AdminAccount
            modelBuilder.Entity<AdminAccount>().HasData(new AdminAccount()
            {
                Id = 1001,
                Active = true,
                CreatedTime = DateTime.Now,
                Password = "7828d7aa6efcf983b850025a6ceccad25905f5ecfa1758edbd1715d012747f2e",
                Search = "admin dion@info.vn",
                UserName = "admin",
                Photo = AdminAccountConst.DEFAULT_PHOTO,
                AccessFailedCount = 0,
                LockEnabled = false,
                Email = "dion@info.vn",
                FullName = "admin",
                Phone = "0123456789",
            });
            #endregion

            #region Candidate_Level
            modelBuilder.Entity<CandidateLevel>().HasData(new CandidateLevel()
            {
                Active = true,
                CreatedTime = DateTime.Now,
                Name = "Thường",
                Id = 1001,
                Price = 0,
                DiscountPrice = 0,
                ExpiryTime = 0
            });
            #endregion

            #region Position

            modelBuilder.Entity<Position>().HasData(new Position()
            {
                Id = 1001,
                Active = true,
                CreatedTime = DateTime.Now,
                Name = "Nhân viên",
            });
            #endregion

            #region Employer_Service_Package

            //modelBuilder.Entity<EmployerServicePackage>().HasData(new EmployerServicePackage()
            //{
            //    Id = 1001,
            //    Active = true,
            //    CreatedTime = DateTime.Now,
            //    DiscountPrice = 0,
            //    Name = "Thường",
            //    Price = 0,
            //    //ServicePackageGroupId = null,
            //    //ServicePackageTypeId = null
            //});
            #endregion

            #region CVTemplate_Status
            modelBuilder.Entity<CVTemplateStatus>().HasData(new CVTemplateStatus()
            {
                Id = 1001,
                Active = true,
                CreatedTime = DateTime.Now,
                Name = "Publish",
                Description = "Template đi vào hoạt động chính thức"
            });
            modelBuilder.Entity<CVTemplateStatus>().HasData(new CVTemplateStatus()
            {
                Id = 1002,
                Active = true,
                CreatedTime = DateTime.Now,
                Name = "Draft",
                Description = "Template nháp. Vì một lý do nào đó nên chưa được đưa vào chính thức"
            });
            #endregion
        }
    }
}
