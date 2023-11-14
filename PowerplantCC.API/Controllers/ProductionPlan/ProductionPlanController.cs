using Microsoft.AspNetCore.Mvc;

namespace PowerplantCC.API.Controllers.ProductionPlan
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProductionPlanController : ControllerBase
    {
        [HttpPost(Name = ProductionPlanRoutes.ProductionPlan)]
        public IActionResult CalculateProductionPlanAsync(ProductionPlanPayloadDto productionPlanPayload)
        {

            return Ok(productionPlanPayload);
        }
    }
}
