using PowerplantCC.API.Controllers.ProductionPlan.Models;

namespace PowerplantCC.API.Services;

public class PowerPlantLoadBalancerService : IPowerPlantLoadBalancerService
{
    public IEnumerable<PowerplantLoadDto> BalanceLoadConfiguration(ProductionPlanPayloadDto productionPlanPayload)
    {
        var powerplants = productionPlanPayload.PowerPlants.OrderBy(p => CalculatePowerplantCost(p, productionPlanPayload.Fuels));
        var remainingLoad = productionPlanPayload.Load;
        var powerplantLoads = new List<PowerplantLoadDto>();

        foreach (var powerplant in powerplants)
        {
            var powerplantLoad = new PowerplantLoadDto(powerplant.Name, CalculatePowerplantOutput(powerplant, productionPlanPayload.Fuels, remainingLoad));
            powerplantLoads.Add(powerplantLoad);
            remainingLoad -= powerplantLoad.P;
        }

        if (remainingLoad < 0)
        {
            powerplants = productionPlanPayload.PowerPlants.OrderByDescending(p => CalculatePowerplantCost(p, productionPlanPayload.Fuels));
            foreach (var powerplant in powerplants)
            {
                var powerplantLoad = powerplantLoads.Single(pl => pl.Name == powerplant.Name);
                if (powerplantLoad.P > 0)
                {
                    var loadReduction = Math.Min(-remainingLoad, powerplantLoad.P - powerplant.PMin);
                    powerplantLoad.P -= loadReduction;
                    remainingLoad -= loadReduction;
                }
                if (remainingLoad == 0)
                {
                    return powerplantLoads;
                }
            }

        }

        if (remainingLoad > 0)
        {
            throw new BadHttpRequestException("Unable to meet load requirements with the given powerplants");
        }

        return powerplantLoads;
    }

    private static double CalculatePowerplantOutput(PowerplantDto powerplant, FuelsDto fuelsDto, double load)
    {
        if (load <= 0)
        {
            return 0;
        }

        return powerplant.Type switch
        {
            Domain.Enums.PowerplantType.GasFired or Domain.Enums.PowerplantType.TurboJet => Math.Round(Math.Max(Math.Min(powerplant.PMax, load), powerplant.PMin), 1),
            Domain.Enums.PowerplantType.WindTurbine => Math.Round(Math.Min(powerplant.PMax * (fuelsDto.WindPercentage / 100), load), 1),
            _ => throw new NotImplementedException($"{powerplant.Type} not implemented"),
        };
    }

    private static double CalculatePowerplantCost(PowerplantDto powerplant, FuelsDto fuels)
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
