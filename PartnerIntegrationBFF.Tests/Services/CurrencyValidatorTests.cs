using PartnerIntegrationBFF.API.Services;

namespace PartnerIntegrationBFF.Tests.Services;

public class CurrencyValidatorTests
{
    private readonly CurrencyValidator _validator = new();

    [Fact]
    public void IsValid_Should_Return_True_When_Currency_Is_Supported()
    {
        // Act
        var result = _validator.IsValid("AUD");
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void IsValid_Should_Return_False_When_Currency_Is_Unsupported()
    {
        // Act
        var result = _validator.IsValid("XYZ");
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void IsValid_Should_Be_Case_Insensitive()
    {
        // Act
        var result = _validator.IsValid("usd");
        
        // Assert
        Assert.True(result);
    }
}