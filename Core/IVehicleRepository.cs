using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
         void Add(Vehicle vehcile);
         void Remove(Vehicle vehicle);
         Task<IEnumerable<Vehicle>> GetVehicles(bool includeRelated = true);
    }
}