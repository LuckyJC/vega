using Microsoft.AspNetCore.Mvc;
using vega.Models;

namespace vega.Controllers
{
    //apply route attribute here and it will apply to all actions in this controller
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        [HttpPost]
        public IActionResult CreateVehicle([FromBody] Vehicle vehicle)
        {
            return Ok(vehicle);
        }
    }
}