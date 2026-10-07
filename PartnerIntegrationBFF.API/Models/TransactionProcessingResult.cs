namespace PartnerIntegrationBFF.API.Models;

public enum TransactionProcessingStatus
{
    Accepted,
    PartnerNotFoundOrInactive,
    PartnerVerificationUnavailable,
    PartnerNotVerified,
    Duplicate
}

public record TransactionProcessingResult(
    TransactionProcessingStatus Status, 
    long? TransactionId = null
);