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
    public class CandidateSuggestionWorkPlaceConfiguration : IEntityTypeConfiguration<CandidateSuggestionWorkPlace>
    {
        public void Configure(EntityTypeBuilder<CandidateSuggestionWorkPlace> builder)
        {
            builder.ToTable("CandidateSuggestionWorkPlace");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.CandidateId).HasComment("Mã ứng viên");
            builder.Property(e => e.WorkPlaceId).HasComment("Mã nơi làm việc");
            builder.Property(e => e.Active)
                 .IsRequired()
                 .HasDefaultValueSql("((1))")
                 .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateSuggestionWorkPlaces)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateSuggestionWorkPlace_Candidate");

            builder.HasOne(d => d.WorkPlace).WithMany(p => p.CandidateSuggestionWorkPlaces)
                .HasForeignKey(d => d.WorkPlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateSuggestionWorkPlace_WorkPlace");
        }
    }
}
