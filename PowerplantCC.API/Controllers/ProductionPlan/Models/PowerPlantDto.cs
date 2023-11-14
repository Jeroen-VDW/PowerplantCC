using PowerplantCC.Domain.Enums;
using System.Text.Json.Serialization;

namespace PowerplantCC.API.Controllers.ProductionPlan
{
    public partial class ProductionPlanController
    {
        public record PowerPlantDto(
            string Name,
            [property: JsonConverter(typeof(JsonStringEnumConverter))]
            PowerplantType PowerplantType,
            double Efficiency,
            double PMin,
            double PMax);
    }
}
