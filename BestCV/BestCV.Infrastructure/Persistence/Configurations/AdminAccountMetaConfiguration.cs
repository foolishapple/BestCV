using BestCV.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Persistence.Configurations
{
	public class AdminAccountMetaConfiguration : IEntityTypeConfiguration<AdminAccountMeta>
    {
        public void Configure(EntityTypeBuilder<AdminAccountMeta> builder)
	{
		builder.ToTable("AdminAccountMeta");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.IsRequired()
			.ValueGeneratedOnAdd()
			.UseIdentityColumn(1001, 1);
		builder.Property(x => x.Active)
			.HasComment("Đánh dấu bị xóa")
			.HasDefaultValueSql("((1))")
			.IsRequired();
		builder.Property(x => x.CreatedTime)
			.HasDefaultValueSql("(getdate())")
			.HasComment("Ngày tạo");
		builder.Property(x => x.Description)
			.HasComment("Mô tả");
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(255)
			.HasComment("Tên dữ liệu bổ sung của Admin");

		builder.Property(x => x.AdminAccountId)
			.IsRequired()
			.HasComment("Mã Admin");

		builder.Property(x => x.Key)
			.IsRequired()
			.HasMaxLength(255)
			.HasComment("Tên khóa");
		builder.Property(x => x.Value)
			.IsRequired()
			.HasMaxLength(255)
			.HasComment("Giá trị");

		builder.HasOne(x => x.AdminAccount).WithMany(x => x.AdminAccountMetas)
			.HasForeignKey(x => x.AdminAccountId)
			.OnDelete(DeleteBehavior.ClientSetNull)
			.HasConstraintName("FK_AdminAccountMeta_AdminAccount");

	}
}
}
