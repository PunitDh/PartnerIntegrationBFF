using Microsoft.EntityFrameworkCore;
using PartnerIntegrationBFF.API.Data;
using PartnerIntegrationBFF.API.Entities;

namespace PartnerIntegrationBFF.API.Repositories;

public class PartnerRepository(AppDbContext dbContext) : IPartnerRepository
{
    public async Task<Partner?> GetActivePartnerByCodeAsync(string partnerCode, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Partners
            .Where(partner => partner.IsActive)
            .SingleOrDefaultAsync(
                partner => partner.PartnerCode == partnerCode, 
                cancellationToken
            );
    }
}