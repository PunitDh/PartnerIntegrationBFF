namespace PartnerIntegrationBFF.API.Entities;

public class PartnerTransaction
{
    public long Id { get; set; }

    public string TransactionReference { get; set; } = string.Empty;
    
    public decimal Amount { get; set; }

    public string Currency { get; set; } = string.Empty;
    
    public DateTime Timestamp { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    
    public int PartnerId { get; set; }

    public Partner Partner { get; set; } = null!;
}