using PartnerIntegrationBFF.API.Models;

namespace PartnerIntegrationBFF.API.Services;

public interface ITransactionService
{
    Task<TransactionProcessingResult> ProcessAsync(
        PartnerTransactionRequest request, 
        CancellationToken cancellationToken = default
    );
}