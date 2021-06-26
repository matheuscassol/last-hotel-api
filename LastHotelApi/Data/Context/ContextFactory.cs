using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<HotelContext>
    {
        public HotelContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HotelContext>();
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = LastHotelDb; Trusted_Connection = True;");

            return new HotelContext(optionsBuilder.Options);
        }
    }
}
