namespace PowerplantCC.API.Controllers.ProductionPlan
{
    public partial class ProductionPlanController
    {
        public record ProductionPlanResponseDto(PowerplantLoad[] PowerplantLoads);
    }
}
