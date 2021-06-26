using Data.Mapping;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class HotelContext: DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public HotelContext(DbContextOptions<HotelContext> options): base (options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientEntity>(new ClientMapping().Configure);
            modelBuilder.Entity<BookingEntity>(new BookingMapping().Configure);
        }
    }
}
