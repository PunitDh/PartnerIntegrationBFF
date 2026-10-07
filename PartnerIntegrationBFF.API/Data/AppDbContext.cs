using Microsoft.EntityFrameworkCore;
using PartnerIntegrationBFF.API.Entities;

namespace PartnerIntegrationBFF.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Partner> Partners => Set<Partner>();

    public DbSet<PartnerTransaction> Transactions => Set<PartnerTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.PartnerCode)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(p => p.PartnerCode)
                .IsUnique();

            entity.HasData(new
            {
                Id = 1,
                PartnerCode = "P-1001",
                Name = "AcmeCorp",
                IsActive = true
            }, new
            {
                Id = 2,
                PartnerCode = "P-1002",
                Name = "Globex",
                IsActive = true
            }, new
            {
                Id = 3,
                PartnerCode = "P-1003",
                Name = "Umbrella",
                IsActive = false
            });
        });

        modelBuilder.Entity<PartnerTransaction>(entity =>
        {
            entity.HasKey(transaction => transaction.Id);

            entity.Property(transaction => transaction.TransactionReference)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(transaction => transaction.Currency)
                .IsRequired()
                .HasMaxLength(3);

            entity.Property(transaction => transaction.Amount)
                .HasPrecision(18, 2);

            entity.HasOne(transaction => transaction.Partner)
                .WithMany(partner => partner.Transactions)
                .HasForeignKey(transaction => transaction.PartnerId);

            entity.HasIndex(transaction => new
            {
                transaction.PartnerId,
                transaction.TransactionReference
            }).IsUnique();
        });
    }
}