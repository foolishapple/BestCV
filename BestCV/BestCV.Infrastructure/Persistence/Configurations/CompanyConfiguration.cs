using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã công ty");
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasComment("Tên công ty");
            builder.Property(x => x.Active)
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(x => x.Description)
                .HasComment("Mô tả")
                .HasMaxLength(5000);
            builder.Property(x => x.CreatedTime)
                .HasDefaultValueSql("getdate()")
                .HasComment("Ngày tạo");
            builder.Property(x => x.Search)
                .HasComment("Tìm kiếm tổng")
                .IsRequired();

            builder.Property(x => x.EmployerId)
                .HasComment("Mã nhà tuyển dụng")
                .IsRequired();

            builder.Property(x => x.CompanySizeId)
                .HasComment("Mã quy mô công ty")
                .IsRequired();
            builder.Property(x => x.EmailAddress)
                .HasMaxLength(500)
                .HasComment("Địa chỉ email của công ty");
            builder.Property(x => x.AddressDetail)
                .HasComment("Địa chỉ công ty")
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Location)
                .HasComment("Tọa độ trên bản đồ")
                .HasMaxLength(500);

            builder.Property(x => x.Website)
                .IsRequired()
                .HasMaxLength(500)
                .HasComment("Trang web của công ty");

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("Số điện thoại của công ty");

            builder.Property(x => x.Logo)
                .IsRequired()
                .HasMaxLength(500)
                .HasComment("Logo công ty");

            builder.Property(x => x.CoverPhoto)
                .IsRequired()
                .HasMaxLength(500)
                .HasComment("Ảnh bìa");

            builder.Property(x => x.TaxCode)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Mã số thuế");

            builder.Property(x => x.FoundedIn)
                .IsRequired()
                .HasComment("Năm thành lập");

            builder.Property(x => x.TiktokLink)
                .HasMaxLength(500)
                .HasComment("Tài khoản Tiktok");
            builder.Property(x => x.YoutubeLink)
                .HasMaxLength(500)
                .HasComment("Tài khoản Youtube");
            builder.Property(x => x.FacebookLink)
                .HasMaxLength(500)
                .HasComment("Tài khoản Facebook");
            builder.Property(x => x.TwitterLink)
                .HasMaxLength(500)
                .HasComment("Tài khoản Twitter");
            builder.Property(x => x.LinkedinLink)
                .HasMaxLength(500)
                .HasComment("Tài khoản Linkedin");
            builder.Property(x => x.VideoIntro)
               .HasMaxLength(500)
               .HasComment("Link video giới thiệu");
            builder.Property(x => x.WorkPlaceId)
                .HasComment("Mã nơi làm việc");
            builder.Property(x => x.Overview).HasMaxLength(255).HasComment("Tổng quan");

            builder.HasIndex(x => x.TaxCode).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.Website).IsUnique();

            builder.HasOne(x => x.CompanySize).WithMany(x => x.Companies)
                .HasForeignKey(x => x.CompanySizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_CompanySize");

            builder.HasOne(x => x.Employer).WithOne(x => x.Company)
                .HasForeignKey<Company>(x => x.EmployerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Employer");

            builder.HasOne(x => x.WorkPlace).WithMany(x => x.Companies)
                .HasForeignKey(x => x.WorkPlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_WorkPlace");

        }
    }
}
