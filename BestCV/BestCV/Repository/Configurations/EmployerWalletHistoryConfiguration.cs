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
    public class EmployerWalletHistoryConfiguration : IEntityTypeConfiguration<EmployerWalletHistory>
    {
        public void Configure(EntityTypeBuilder<EmployerWalletHistory> builder)
        {
            #region Properties
            builder.Property(c => c.Id).UseIdentityColumn(1001, 1).ValueGeneratedOnAdd().HasComment("Mã");
            builder.Property(c => c.Name).HasMaxLength(255).HasComment("Tên");
            builder.Property(c => c.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(c => c.Active).HasDefaultValue(true).HasComment("Đánh dấu đã bị xóa");
            builder.Property(c => c.CreatedTime).HasDefaultValueSql("(getdate())").HasComment("Ngày tạo");
            builder.Property(c => c.Amount).HasComment("Giá trị");
            builder.Property(c => c.EmployerWalletId).HasComment("Mã ví nhà tuyển dụng");
            builder.Property(c => c.WalletHistoryTypeId).HasComment("Mã loại lịch sử ví");
            builder.Property(c => c.CandidateId).HasComment("Mã ứng viên");
            builder.Property(c => c.IsApproved).HasComment("Trạng thái duyệt");
            builder.Property(c => c.UpdatedTime).HasComment("Ngày cập nhật");
            #endregion
            #region Relationship   
            builder.HasKey(c => c.Id).HasName("PK_EmployerWalletHistory");
            builder.HasOne(c => c.EmployerWallet).WithMany(c => c.EmployerWalletHistories).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(c => c.EmployerWalletId).HasConstraintName("FK_EmployerWalletHistory_EmployerWallet");
            builder.HasOne(c => c.WalletHistoryType).WithMany(c => c.EmployerWalletHistories).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(c => c.WalletHistoryTypeId).HasConstraintName("FK_EmployerWalletHistory_WalletHistoryType");
            builder.HasOne(c => c.Candidate).WithMany(c => c.EmployerWalletHistories).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(c => c.CandidateId).HasConstraintName("FK_EmployerWalletHistory_Candidate");
            #endregion
        }
    }
}
