using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HomiesAPI.Models;

namespace HomiesAPI.DataAccess
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

        public virtual DbSet<Homies> Homies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Homies>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
