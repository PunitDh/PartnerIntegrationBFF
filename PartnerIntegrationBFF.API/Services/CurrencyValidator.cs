namespace PartnerIntegrationBFF.API.Services;

public class CurrencyValidator: ICurrencyValidator
{
    private static readonly List<SupportedCurrency> SupportedCurrencies =
    [
        new("AUD", "Australian Dollar"),
        new("USD", "United States Dollar"),
        new("EUR", "Euro"),
        new("GBP", "British Pound"),
        new("JPY", "Japanese Yen")
    ];
    
    public bool IsValid(string currencyCode)
    {
        var currency =
            SupportedCurrencies.SingleOrDefault(code =>
                code.Code.Equals(currencyCode, StringComparison.OrdinalIgnoreCase));

        return currency is not null;
    }

    private record SupportedCurrency(string Code, string Name);
}