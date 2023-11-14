namespace PowerplantCC.API.Controllers.ProductionPlan
{
    public partial class ProductionPlanController
    {
        public record ProductionPlanPayloadDto(double Load, FuelsDto Fuels, PowerPlantDto[] PowerPlants);
    }
}
