namespace PowerplantCC.API.Controllers.ProductionPlan.Models;

public record PowerplantLoadDto(string Name, double P)
{
    public double P { get; set; } = P;
}
