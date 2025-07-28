using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHCardHelper.Models.Cards;
using DHCardHelper.Models.Domains;

namespace DHCardHelper.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Card> _cards { get; set; }
        public DbSet<AvailableDomain> _domains { get; set; }
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

            modelBuilder.Entity<AvailableDomain>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
