using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
    public class BookingMapping : IEntityTypeConfiguration<BookingEntity>
    {
        public void Configure(EntityTypeBuilder<BookingEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.StartDate)
                .IsUnique();

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.HasIndex(x => x.EndDate)
                .IsUnique();

            builder.Property(x => x.EndDate)
                .IsRequired();

            builder
                .HasOne(b => b.Client)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ClientId)
                .IsRequired();
        }
    }
}
