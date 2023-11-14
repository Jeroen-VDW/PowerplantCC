using PowerplantCC.Domain.Enums;

namespace PowerplantCC.API.Controllers.ProductionPlan
{
    public partial class ProductionPlanController
    {
        public record PowerPlantDto(string Name, PowerplantType PowerplantType, double Efficiency, double PMin, double PMax);
    }
}
