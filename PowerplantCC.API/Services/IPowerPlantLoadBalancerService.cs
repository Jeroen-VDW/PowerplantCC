using PowerplantCC.API.Controllers.ProductionPlan.Models;

namespace PowerplantCC.API.Services;

public interface IPowerPlantLoadBalancerService
{
    IEnumerable<PowerplantLoadDto> BalanceLoadConfiguration(ProductionPlanPayloadDto productionPlanPayload);
}