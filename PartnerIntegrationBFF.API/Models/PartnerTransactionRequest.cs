using System.ComponentModel.DataAnnotations;

namespace PartnerIntegrationBFF.API.Models;

public class PartnerTransactionRequest
{
    [Required]
    public string PartnerId { get; set; } = string.Empty;
    
    [Required]
    public string TransactionReference { get; set; } = string.Empty;
    
    [Range(typeof(decimal), "0.01", "1000000000")]
    public decimal Amount { get; set; }
    
    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; } = string.Empty;
    
    [Required]
    public DateTime Timestamp { get; set; }
}