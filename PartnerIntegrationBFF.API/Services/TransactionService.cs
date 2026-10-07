using Microsoft.EntityFrameworkCore;
using PartnerIntegrationBFF.API.Clients;
using PartnerIntegrationBFF.API.Data;
using PartnerIntegrationBFF.API.Entities;
using PartnerIntegrationBFF.API.Models;
using PartnerIntegrationBFF.API.Repositories;

namespace PartnerIntegrationBFF.API.Services;

public class TransactionService(
    AppDbContext dbContext, 
    IPartnerRepository partnerRepository,
    IPartnerVerificationClient partnerVerificationClient
) : ITransactionService
{
    public async Task<TransactionProcessingResult> ProcessAsync(PartnerTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var partner = await partnerRepository.GetActivePartnerByCodeAsync(request.PartnerId, cancellationToken);

        if (partner is null) return new TransactionProcessingResult(TransactionProcessingStatus.PartnerNotFoundOrInactive);

        bool isVerified;

        try
        {
            isVerified = await partnerVerificationClient.VerifyPartnerAsync(request.PartnerId, cancellationToken);
        }
        catch (HttpRequestException)
        {
            return new TransactionProcessingResult(TransactionProcessingStatus.PartnerVerificationUnavailable);
        }

        if (!isVerified) return new TransactionProcessingResult(TransactionProcessingStatus.PartnerNotVerified);
        
        var alreadyExists = await dbContext.Transactions.AnyAsync(transaction =>
            transaction.PartnerId == partner.Id && transaction.TransactionReference == request.TransactionReference, 
            cancellationToken
        );

        if (alreadyExists) return new TransactionProcessingResult(TransactionProcessingStatus.Duplicate);

        var transaction = new PartnerTransaction
        {
            PartnerId = partner.Id,
            TransactionReference = request.TransactionReference,
            Amount = request.Amount,
            Currency = request.Currency,
            Timestamp = request.Timestamp,
            CreatedAtUtc = DateTime.UtcNow
        };

        dbContext.Transactions.Add(transaction);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new TransactionProcessingResult(
            TransactionProcessingStatus.Accepted,
            transaction.Id
        );
    }
}