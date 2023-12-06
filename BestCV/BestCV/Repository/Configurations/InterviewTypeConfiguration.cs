﻿using Jobi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Persistence.Configurations
{
    public class InterviewTypeConfiguration : IEntityTypeConfiguration<InterviewType>
    {
        public void Configure(EntityTypeBuilder<InterviewType> builder)
        {
            builder.ToTable("InterviewType");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã loại phỏng vấn")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description).HasComment("Mô tả");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên loại phỏng vấn");
        }
    }
}
