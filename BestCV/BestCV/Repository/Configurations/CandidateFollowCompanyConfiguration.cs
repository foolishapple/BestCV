using Jobi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Persistence.Configurations
{
    public class CandidateFollowCompanyConfiguration : IEntityTypeConfiguration<CandidateFollowCompany>
    {
        public void Configure(EntityTypeBuilder<CandidateFollowCompany> builder)
        {
            builder.ToTable("CandidateFollowCompany");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã ứng viên theo dỗi công ty");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateFollowCompanies)
               .HasForeignKey(d => d.CandidateId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateFollowCompany_Candidate");

            builder.HasOne(d => d.Company).WithMany(p => p.CandidateFollowCompanies)
               .HasForeignKey(d => d.CompanyId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateFollowCompany_Company");
        }
    }
}
