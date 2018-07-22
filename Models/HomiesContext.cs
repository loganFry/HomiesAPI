﻿using System;
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

        public DbSet<Homie> Homies { get; set; }

        public DbSet<CheckIn> CheckIns { get; set; }

        public DbSet<CheckOut> CheckOuts { get; set; }
    }
}
