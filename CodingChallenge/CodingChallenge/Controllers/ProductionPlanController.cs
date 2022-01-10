using CodingChallengeWeb.Models;
using CodingChallengeWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallengeWeb.Controllers
{
    public class ProductionPlanController : Controller
    {
        [HttpPost("productionplan")]
        public IActionResult PostPlayload([FromBody] Playload playload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = ProductionPlanServices.Calculate(playload);
                return Json(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
