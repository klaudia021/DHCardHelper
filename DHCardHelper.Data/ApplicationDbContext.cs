using Microsoft.EntityFrameworkCore;
using DHCardHelper.Models.Entities;
using DHCardHelper.Models.Entities.Cards;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DHCardHelper.Models.Entities.Users;
using DHCardHelper.Models.Entities.Characters;
using DHCardHelper.Models.Entities.Relationships;

namespace DHCardHelper.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<DomainCardType> DomainCardTypes { get; set; }
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<BackgroundCardType> BackgroundCardTypes { get; set; }
        public DbSet<CharacterSheet> CharacterSheet { get; set; }
        public DbSet<CardSheet> CardSheet { get; set; }
        public DbSet<ClassToDomainRel> ClassToDomainRel { get; set; }
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

            modelBuilder.Entity<Card>()
                .HasOne(c => c.Domain)
                .WithMany()
                .HasForeignKey(c => c.DomainId)
                .IsRequired(false);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.DomainCardType)
                .WithMany()
                .HasForeignKey(c => c.DomainCardTypeId)
                .IsRequired(false);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.CharacterClass)
                .WithMany()
                .HasForeignKey(c => c.CharacterClassId)
                .IsRequired(false);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.BackgroundType)
                .WithMany()
                .HasForeignKey(c => c.BackgroundTypeId)
                .IsRequired(false);

            modelBuilder.Entity<SubclassCard>()
                .Property(m => m.MasteryType)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
