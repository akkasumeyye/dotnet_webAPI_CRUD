using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HotelFinder.Data
{
    
        public class HotelDbContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=HotelDbx");
            }

            public DbSet<Hotel> Hotels { get; set; }
        }

    
}