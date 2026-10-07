using Microsoft.AspNetCore.Mvc;
using PartnerIntegrationBFF.API.Models;
using PartnerIntegrationBFF.API.Services;

namespace PartnerIntegrationBFF.API.Controllers;

[ApiController]
[Route("api/v1/partner/transactions")]
public class PartnerTransactionsController(ICurrencyValidator currencyValidator) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateTransaction([FromBody] PartnerTransactionRequest request)
    {
        if (!currencyValidator.IsValid(request.Currency))
        {
            return BadRequest(new
            {
                error = $"Unsupported currency: '{request.Currency}'."
            });
        }
        return Ok(request);
    }
}