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
    public class CompanyFieldOfActivityConfiguration : IEntityTypeConfiguration<CompanyFieldOfActivity>
    {
        public void Configure(EntityTypeBuilder<CompanyFieldOfActivity> builder)
        {
            builder.ToTable("CompanyFieldOfActivity");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã lĩnh vực hoạt động của công ty");
            builder.Property(e => e.CompanyId).IsRequired();
            builder.Property(e => e.FieldOfActivityId).IsRequired();
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(x=> x.Company).WithMany(x=> x.CompanyFieldOfActivities)
                .HasForeignKey(x=>x.CompanyId)
                .HasConstraintName("FK_CompanyFieldOfActivity_Company")
                .OnDelete(DeleteBehavior.ClientSetNull);



            builder.HasOne(x => x.FieldOfActivity).WithMany(x => x.CompanyFieldOfActivities)
                .HasForeignKey(x => x.FieldOfActivityId)
                .HasConstraintName("FK_CompanyFieldOfActivity_FieldOfActivity")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
