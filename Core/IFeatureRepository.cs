using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Controllers.Resources;
using vega.Core.Models;

namespace vega.Core
{
    public interface IFeatureRepository
    {
         Task<IEnumerable<Feature>> GetFeatures();
    }
}