using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Core.Models;

namespace vega.Persistence
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly VegaDbContext _context;
        public FeatureRepository(VegaDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Feature>> GetFeatures()
        {
            return await _context.Features.ToListAsync();
        }

    }
}