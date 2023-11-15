namespace PowerplantCC.API.Controllers.ProductionPlan.Models;

public record PowerplantLoad(string Name, double P)
{
    public double P { get; set; } = P;
}
