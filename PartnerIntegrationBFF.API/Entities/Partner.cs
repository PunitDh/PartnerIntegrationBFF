namespace PartnerIntegrationBFF.API.Entities;

public class Partner
{
    public int Id { get; set; }

    public string PartnerCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    
    public bool IsActive { get; set; }

    public ICollection<PartnerTransaction> Transactions { get; set; } = new List<PartnerTransaction>();
}