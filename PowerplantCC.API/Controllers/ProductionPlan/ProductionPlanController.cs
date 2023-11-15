using Microsoft.AspNetCore.Mvc;
using PowerplantCC.API.Controllers.ProductionPlan.Models;
using PowerplantCC.API.Services;

namespace PowerplantCC.API.Controllers.ProductionPlan;

[Route("api/[controller]")]
[ApiController]
public partial class ProductionPlanController : ControllerBase
{
    private readonly ILogger<ProductionPlanController> logger;
    private readonly IPowerPlantLoadBalancerService powerPlantLoadBalancerService;

    public ProductionPlanController(ILogger<ProductionPlanController> logger, IPowerPlantLoadBalancerService powerPlantLoadBalancerService)
    {
        this.logger = logger;
        this.powerPlantLoadBalancerService = powerPlantLoadBalancerService;
    }

    [HttpPost(Name = ProductionPlanRoutes.ProductionPlan)]
    public IActionResult CalculateProductionPlanAsync(ProductionPlanPayloadDto productionPlanPayload)
    {
        try
        {
            var powerplantLoads = powerPlantLoadBalancerService.BalanceLoadConfiguration(productionPlanPayload);
            return Ok(powerplantLoads);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
