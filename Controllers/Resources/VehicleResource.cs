using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace vega.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }

        public ModelResource Model { get; set; }

        public MakeResource Make { get; set; }

        public bool IsRegistered { get; set; }
        
        public ContactResource Contact { get; set; }
        
        public DateTime LastUpdate { get; set; }

        public ICollection<FeatureResource> Features { get; set; }

        //best practice to set a constructor to initialize features
        public VehicleResource()
        {
            Features = new Collection<FeatureResource>();
        }

    }
}