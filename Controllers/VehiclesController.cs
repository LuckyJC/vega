using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using vega.Models;

namespace vega.Controllers
{
    //apply route attribute here and it will apply to all actions in this controller
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        public IMapper _mapper { get; }
        public VehiclesController(IMapper _mapper)
        {
            this._mapper = _mapper;
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] VehicleResource vehicleResource)
        {
            var vehicle = _mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            return Ok(vehicle);
        }
    }
}