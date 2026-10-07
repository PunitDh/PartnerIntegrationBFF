using PartnerIntegrationBFF.API.Models;

namespace PartnerIntegrationBFF.API.Clients;

public class PartnerVerificationClient(HttpClient httpClient) : IPartnerVerificationClient
{
    public async Task<bool> VerifyPartnerAsync(string partnerId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"api/mock/partners/{partnerId}/verify", cancellationToken);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content.ReadFromJsonAsync<PartnerVerificationResponse>(cancellationToken: cancellationToken);

        return (bool) result?.IsVerified;
    }
}