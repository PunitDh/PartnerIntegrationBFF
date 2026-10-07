using Microsoft.AspNetCore.Mvc;
using PartnerIntegrationBFF.API.Models;

namespace PartnerIntegrationBFF.API.Controllers;

[ApiController]
[Route("api/mock/partners")]
public class PartnerVerificationController : ControllerBase
{
    [HttpGet("{partnerId}/verify")]
    public ActionResult<PartnerVerificationResponse> VerifyPartner(string partnerId)
    {
        var random = (uint) Math.Round(Random.Shared.NextDouble() * 100);
        
        return random < 30
            ? throw new TimeoutException($"The mock partner verification API has timed out with value: {random}") 
            : Ok(new PartnerVerificationResponse(partnerId, true));
    }
}