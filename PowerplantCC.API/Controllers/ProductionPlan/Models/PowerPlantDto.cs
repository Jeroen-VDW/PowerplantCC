using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PowerplantCC.Domain.Enums;

namespace PowerplantCC.API.Controllers.ProductionPlan.Models;

public record PowerplantDto(
    string Name,
    [JsonConverter(typeof(StringEnumConverter))]
        PowerplantType? Type,
    double Efficiency,
    double PMin,
    double PMax);
