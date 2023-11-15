namespace PowerplantCC.API.Controllers.ProductionPlan
{
    public partial class ProductionPlanController
    {
        public record PowerplantLoad(string Name, double P)
        {
            public double P { get; set; } = P;
        }
    }
}
