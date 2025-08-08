using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;

namespace DHCardHelper.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Models.Entities.Type> Types { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasDiscriminator<string>("CardType")
                .HasValue<BackgroundCard>("Background")
                .HasValue<DomainCard>("Domain")
                .HasValue<SubclassCard>("Subclass");

            modelBuilder.Entity<Domain>();
            modelBuilder.Entity<Models.Entities.Type>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
