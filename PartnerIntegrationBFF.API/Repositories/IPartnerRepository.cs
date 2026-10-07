using PartnerIntegrationBFF.API.Entities;

namespace PartnerIntegrationBFF.API.Repositories;

public interface IPartnerRepository
{
    Task<Partner?> GetActivePartnerByCodeAsync(string partnerCode, CancellationToken cancellationToken = default);
}