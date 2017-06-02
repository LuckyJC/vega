using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
    //apply route attribute here and it will apply to all actions in this controller
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        public VegaDbContext _context { get; }
        public IMapper _mapper { get; }
        public VehiclesController(VegaDbContext _context, IMapper _mapper)
        {
            this._context = _context;
            this._mapper = _mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //This is an example of how we could add custom validation for business logic; not really necessary for this application
            //but serves as an example of how we can do business logic
            var model = await _context.Models.FindAsync(vehicleResource.ModelId);
            if(model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }

            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            //adds complete vehicle object in memory and includes features, models, and makes
            vehicle = await _context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature) //new method in ef core allows us to eager load nested objects
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make) //works with mapping profile to eager load make with model
                .SingleOrDefaultAsync(v => v.Id == vehicle.Id);

            var result = _mapper.Map<Vehicle, SaveVehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //This is an example of how we could add custom validation for business logic; not really necessary for this application
            //but serves as an example of how we can do business logic
            var model = await _context.Models.FindAsync(vehicleResource.ModelId);
            if(model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }

            //provides a complete representation of vehicle
            var vehicle = await _context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature) //new method in ef core allows us to eager load nested objects
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make) //works with mapping profile to eager load make with model
                .SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await _context.SaveChangesAsync();

            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return NotFound();

            _context.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            //need to eager load Features with vehicle; replace FindAsync with SingleOrDefaultAsync
            var vehicle = await _context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature) //new method in ef core allows us to eager load nested objects
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make) //works with mapping profile to eager load make with model
                .SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }

    }
}