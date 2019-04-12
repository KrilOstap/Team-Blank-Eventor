using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Data.Entities;
using Eventor.Data.Entities;
using Eventor.Data.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventOrganaizer>()
                .HasKey(e => new { e.EventId, e.OrganaizerId });

            modelBuilder.Entity<EventOrganaizer>()
                .HasOne(eo => eo.Organizer)
                .WithMany(o => o.EventOrganaizers)                
                .HasForeignKey(eo => eo.OrganaizerId);                

            modelBuilder.Entity<EventOrganaizer>()
                .HasOne(eo => eo.Event)
                .WithMany(e => e.EventOrganaizers)
                .HasForeignKey(eo => eo.EventId);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<EventOrganaizer> EventOrganaizers { get; set; }
    }
}
