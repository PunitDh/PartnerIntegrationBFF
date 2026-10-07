using Microsoft.EntityFrameworkCore;
using PartnerIntegrationBFF.API.Entities;

namespace PartnerIntegrationBFF.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Partner> Partners => Set<Partner>();

    public DbSet<PartnerTransaction> Transactions => Set<PartnerTransaction>();
}