using Microsoft.AspNetCore.Mvc;

namespace PowerplantCC.API.Controllers.ProductionPlan
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProductionPlanController : ControllerBase
    {
        [HttpPost(Name = ProductionPlanRoutes.ProductionPlan)]
        public IActionResult CalculateProductionPlanAsync(ProductionPlanPayloadDto productionPlanPayload)
        {
            var powerplants = OrderPowerplantsByCost(productionPlanPayload.PowerPlants, productionPlanPayload.Fuels);
            var remainingLoad = productionPlanPayload.Load;
            var powerplantLoads = new List<PowerplantLoad>();

            foreach (var powerplant in powerplants)
            {
                var powerplantLoad = new PowerplantLoad(powerplant.Name, CalculatePowerplantOutput(powerplant, productionPlanPayload.Fuels, remainingLoad));
                powerplantLoads.Add(powerplantLoad);
                remainingLoad -= powerplantLoad.P;
            }

            if (remainingLoad > 0)
            {

                return BadRequest("Unable to meet load requirements with the give powerplants");
            }

            return Ok(powerplantLoads);
        }

        private double CalculatePowerplantOutput(PowerplantDto powerplant, FuelsDto fuelsDto, double load)
        {
            return powerplant.Type switch
            {
                Domain.Enums.PowerplantType.GasFired or Domain.Enums.PowerplantType.TurboJet => Math.Round(Math.Min(powerplant.PMax, load), 1),
                Domain.Enums.PowerplantType.WindTurbine => Math.Round(Math.Min(powerplant.PMax * (fuelsDto.WindPercentage / 100), load), 1),
                _ => throw new NotImplementedException($"{powerplant.Type} not implemented"),
            };
        }

        private IEnumerable<PowerplantDto> OrderPowerplantsByCost(IEnumerable<PowerplantDto> powerplants, FuelsDto fuelsDto)
        {
            return powerplants.OrderBy(p => CalculatePowerplantCost(p, fuelsDto));
        }

        private double CalculatePowerplantCost(PowerplantDto powerplant, FuelsDto fuels)
        {
            if (powerplant.Efficiency == 0)
            {
                return double.MaxValue;
            }

            return powerplant.Type switch
            {
                Domain.Enums.PowerplantType.GasFired => fuels.GasEuroMWh / powerplant.Efficiency,
                Domain.Enums.PowerplantType.TurboJet => fuels.KerosineEuroMWh / powerplant.Efficiency,
                Domain.Enums.PowerplantType.WindTurbine => 0,
                _ => throw new NotImplementedException($"{powerplant.Type} not implemented"),
            };
        }
    }
}
