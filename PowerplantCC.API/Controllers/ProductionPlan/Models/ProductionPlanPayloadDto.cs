namespace PowerplantCC.API.Controllers.ProductionPlan.Models;

public record ProductionPlanPayloadDto(double Load, FuelsDto Fuels, PowerplantDto[] PowerPlants);
