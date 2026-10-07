
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using PartnerIntegrationBFF.API.Clients;
using PartnerIntegrationBFF.API.Models;

namespace PartnerIntegrationBFF.Tests.Clients;

public class PartnerVerificationClientTests
{
    [Fact]
    public async Task VerifyPartnerAsync_Should_Retry_And_Succeed_After_Transient_Failures()
    {
        // Arrange
        var handler = new SequencedHttpMessageHandler();

        var services = new ServiceCollection();

        services.AddHttpClient<IPartnerVerificationClient, PartnerVerificationClient>(client =>
        {
            client.BaseAddress = new Uri("https://fake-partner-api/");
        })
            .ConfigurePrimaryHttpMessageHandler(() => handler)
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.Delay = TimeSpan.Zero;
            });

        await using var serviceProvider = services.BuildServiceProvider();

        var client = serviceProvider.GetRequiredService<IPartnerVerificationClient>();
        
        // Act
        var result = await client.VerifyPartnerAsync("P-1001");
        
        // Assert
        Assert.True(result);
        Assert.Equal(3, handler.CallCount);
    }

    private sealed class SequencedHttpMessageHandler : HttpMessageHandler
    {
        private int _callCount;
        public int CallCount => _callCount;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var attempt = Interlocked.Increment(ref _callCount);

            if (attempt <= 2) return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(
                    new PartnerVerificationResponse("P-1001", true)
                )
            });
        }
    }
}