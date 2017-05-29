using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
    public class FeaturesController
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public FeaturesController(VegaDbContext _context, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._context = _context;

        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();

            return _mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }

    }
}