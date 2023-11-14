using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PowerplantCC.Domain.Enums;

namespace PowerplantCC.API.Controllers.ProductionPlan
{
    public partial class ProductionPlanController
    {
        public record PowerplantDto(
            string Name,
            [JsonConverter(typeof(StringEnumConverter))]
            PowerplantType? Type,
            double Efficiency,
            double PMin,
            double PMax);
    }
}
