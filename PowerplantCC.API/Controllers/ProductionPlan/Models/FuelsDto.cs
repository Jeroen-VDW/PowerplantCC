namespace PowerplantCC.API.Controllers.ProductionPlan
{
    public partial class ProductionPlanController
    {
        public record FuelsDto(double GasEuroMWh, double KerosineEuroMWh, double CO2EuroTon, double WindPercentage);
    }
}
