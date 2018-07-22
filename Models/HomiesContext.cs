using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomiesAPI.Models
{
    public partial class HomiesContext : DbContext
    {
        public HomiesContext()
        {
        }

        public HomiesContext(DbContextOptions<HomiesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationHomie>().HasKey(sc => new { sc.HomieId, sc.LocationId });
        }

        public DbSet<Homie> Homies { get; set; }

        public DbSet<CheckIn> CheckIns { get; set; }

        public DbSet<CheckOut> CheckOuts { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationHomie> LocationHomies { get; set; }
    }
}
