using PowerplantCC.API.Controllers.ProductionPlan.Models;

namespace PowerplantCC.API.Services;

public interface IPowerPlantLoadBalancerService
{
    IEnumerable<PowerplantLoad> BalanceLoadConfiguration(ProductionPlanPayloadDto productionPlanPayload);
}