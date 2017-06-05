using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.Models;

namespace vega.Controllers
{
    public class FeaturesController
    {
        private readonly IMapper _mapper;
        private readonly IFeatureRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public FeaturesController(IMapper _mapper, IFeatureRepository _repository, IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._repository = _repository;
            this._mapper = _mapper;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<Feature>> GetFeatures()
        {
            //var features = await _context.Features.ToListAsync();
            var features = await _repository.GetFeatures();

            return _mapper.Map<IEnumerable<Feature>, IEnumerable<Feature>>(features);
        }

    }
}