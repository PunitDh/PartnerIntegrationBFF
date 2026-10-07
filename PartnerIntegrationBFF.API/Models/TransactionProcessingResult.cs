namespace PartnerIntegrationBFF.API.Models;

public enum TransactionProcessingStatus
{
    Accepted,
    PartnerNotFoundOrInactive,
    Duplicate
}

public record TransactionProcessingResult(
    TransactionProcessingStatus Status, 
    long? TransactionId = null
);