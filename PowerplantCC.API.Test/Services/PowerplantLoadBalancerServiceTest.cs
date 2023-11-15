using FluentAssertions;
using PowerplantCC.API.Controllers.ProductionPlan.Models;
using PowerplantCC.API.Services;
using PowerplantCC.Domain.Enums;

namespace PowerplantCC.API.Test.Services
{
    public class PowerplantLoadBalancerServiceTest
    {
        [Theory]
        [MemberData(nameof(GetBalanceLoadConfigurationTestData))]
        public void BalanceLoadConfiguration_ShouldProduceCorrectResult(ProductionPlanPayloadDto productionPlanPayload, IEnumerable<PowerplantLoadDto> expectedResponse, string? errorMessage)
        {
            // Arrange
            var powerplantLoadBalancerSerivce = new PowerPlantLoadBalancerService();

            // Act
            var action = () => powerplantLoadBalancerSerivce.BalanceLoadConfiguration(productionPlanPayload);

            // Assert
            if (errorMessage == null)
            {
                var result = action();
                result.Sum(r => r.P).Should().Be(productionPlanPayload.Load);
                result.Count().Should().Be(productionPlanPayload.PowerPlants.Length);
                result.Should().BeEquivalentTo(expectedResponse);
            }
            else
            {
                action.Should().Throw<Exception>().WithMessage(errorMessage);
            }
        }

        public static IEnumerable<object?[]> GetBalanceLoadConfigurationTestData()
        {
            yield return new object?[]
            {
                new ProductionPlanPayloadDto(
                    480,
                    new FuelsDto
                    {
                        GasEuroMWh = 13.4,
                        KerosineEuroMWh = 50.8,
                        CO2EuroTon = 20,
                        WindPercentage = 60,
                    },
                    new PowerplantDto[]
                    {
                        new PowerplantDto("gasfiredbig1", PowerplantType.GasFired, .53, 100, 460),
                        new PowerplantDto("gasfiredbig2", PowerplantType.GasFired, .53, 100, 460),
                        new PowerplantDto("gasfiredsomewhatsmaller", PowerplantType.GasFired, .37, 40, 210),
                        new PowerplantDto("tj1", PowerplantType.TurboJet, .3, 0, 16),
                        new PowerplantDto("windpark1", PowerplantType.WindTurbine, 1, 0, 150),
                        new PowerplantDto("windpark2", PowerplantType.WindTurbine, 1, 0, 36),
                    }),
                new List<PowerplantLoadDto>
                {
                    new PowerplantLoadDto("windpark1", 90),
                    new PowerplantLoadDto("windpark2", 21.6),
                    new PowerplantLoadDto("gasfiredbig1", 368.4),
                    new PowerplantLoadDto("gasfiredbig2", 0),
                    new PowerplantLoadDto("gasfiredsomewhatsmaller", 0),
                    new PowerplantLoadDto("tj1", 0),
                },
                null
            };
            yield return new object?[]
            {
                new ProductionPlanPayloadDto(
                    480,
                    new FuelsDto
                    {
                        GasEuroMWh = 13.4,
                        KerosineEuroMWh = 50.8,
                        CO2EuroTon = 20,
                        WindPercentage = 0,
                    },
                    new PowerplantDto[]
                    {
                        new PowerplantDto("gasfiredbig1", PowerplantType.GasFired, .53, 100, 460),
                        new PowerplantDto("gasfiredbig2", PowerplantType.GasFired, .53, 100, 460),
                        new PowerplantDto("gasfiredsomewhatsmaller", PowerplantType.GasFired, .37, 40, 210),
                        new PowerplantDto("tj1", PowerplantType.TurboJet, .3, 0, 16),
                        new PowerplantDto("windpark1", PowerplantType.WindTurbine, 1, 0, 150),
                        new PowerplantDto("windpark2", PowerplantType.WindTurbine, 1, 0, 36),
                    }),
                new List<PowerplantLoadDto>
                {
                    new PowerplantLoadDto("windpark1", 0),
                    new PowerplantLoadDto("windpark2", 0),
                    new PowerplantLoadDto("gasfiredbig1", 380),
                    new PowerplantLoadDto("gasfiredbig2", 100),
                    new PowerplantLoadDto("gasfiredsomewhatsmaller", 0),
                    new PowerplantLoadDto("tj1", 0),
                },
                null
            };
            yield return new object?[]
            {
                new ProductionPlanPayloadDto(
                    910,
                    new FuelsDto
                    {
                        GasEuroMWh = 13.4,
                        KerosineEuroMWh = 50.8,
                        CO2EuroTon = 20,
                        WindPercentage = 60,
                    },
                    new PowerplantDto[]
                    {
                        new PowerplantDto("gasfiredbig1", PowerplantType.GasFired, .53, 100, 460),
                        new PowerplantDto("gasfiredbig2", PowerplantType.GasFired, .53, 100, 460),
                        new PowerplantDto("gasfiredsomewhatsmaller", PowerplantType.GasFired, .37, 40, 210),
                        new PowerplantDto("tj1", PowerplantType.TurboJet, .3, 0, 16),
                        new PowerplantDto("windpark1", PowerplantType.WindTurbine, 1, 0, 150),
                        new PowerplantDto("windpark2", PowerplantType.WindTurbine, 1, 0, 36),
                    }),
                new List<PowerplantLoadDto>
                {
                    new PowerplantLoadDto("windpark1", 90),
                    new PowerplantLoadDto("windpark2", 21.6),
                    new PowerplantLoadDto("gasfiredbig1", 460),
                    new PowerplantLoadDto("gasfiredbig2", 338.4),
                    new PowerplantLoadDto("gasfiredsomewhatsmaller", 0),
                    new PowerplantLoadDto("tj1", 0),
                },
                null
            };
        }
    }
}
