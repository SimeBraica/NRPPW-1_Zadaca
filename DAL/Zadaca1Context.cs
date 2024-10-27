using System;
using System.Collections.Generic;
using DAL.Configurations;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL {
    public class Zadaca1Context : DbContext {

        public Zadaca1Context() { }
        public Zadaca1Context(DbContextOptions dbContextOptions) : base(dbContextOptions) {
        }
        public DbSet<OIB> OIBs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new OIBConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseNpgsql("Host=dpg-crtvqktds78s73faioqg-a.frankfurt-postgres.render.com;Port=5432;Database=nrppw_prva;Username=nrppw_prva_user;Password=RDjUVgRmHefBuCQxa0QmwSOXLMX4VJGK;SSL Mode=Require;Trust Server Certificate=true;");

    }
}