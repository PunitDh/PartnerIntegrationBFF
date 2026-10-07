using Microsoft.AspNetCore.Mvc;
using PartnerIntegrationBFF.API.Models;
using PartnerIntegrationBFF.API.Services;

namespace PartnerIntegrationBFF.API.Controllers;

[ApiController]
[Route("api/v1/partner/transactions")]
public class PartnerTransactionsController(ICurrencyValidator currencyValidator, ITransactionService transactionService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] PartnerTransactionRequest request, CancellationToken cancellationToken)
    {
        if (!currencyValidator.IsValid(request.Currency))
        {
            return BadRequest(new
            {
                error = $"Unsupported currency: '{request.Currency}'."
            });
        }

        var result = await transactionService.ProcessAsync(request, cancellationToken);

        return result.Status switch
        {
            TransactionProcessingStatus.Accepted => Ok(new { transactionId = result.TransactionId }),
            TransactionProcessingStatus.Duplicate => Conflict(new { error = "Transaction has already been processed." }),
            TransactionProcessingStatus.PartnerNotFoundOrInactive => BadRequest(new { error = "Partner does not exist or is inactive" }),
            _ => StatusCode(500)
        };
    }
}