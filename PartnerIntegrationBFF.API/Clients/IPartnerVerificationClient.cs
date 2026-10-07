namespace PartnerIntegrationBFF.API.Clients;

public interface IPartnerVerificationClient
{
    Task<bool> VerifyPartnerAsync(string partnerId, CancellationToken cancellationToken = default);
}