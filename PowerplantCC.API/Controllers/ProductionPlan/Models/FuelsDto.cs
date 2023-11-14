using System.Text.Json.Serialization;

namespace PowerplantCC.API.Controllers.ProductionPlan
{
    public partial class ProductionPlanController
    {
        public class FuelsDto
        {
            [JsonPropertyName("gas(euro/MWh)")]
            public double GasEuroMWh { get; set; }

            [JsonPropertyName("kerosine(euro / MWh)")]
            public double KerosineEuroMWh { get; set; }

            [JsonPropertyName("co2(euro/ton)")]
            public double CO2EuroTon { get; set; }

            [JsonPropertyName("wind(%)")]
            public double WindPercentage { get; set; }
        }
    }
}
