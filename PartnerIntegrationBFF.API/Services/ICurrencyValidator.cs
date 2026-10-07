namespace PartnerIntegrationBFF.API.Services;

public interface ICurrencyValidator
{
    bool IsValid(string currencyCode);
}