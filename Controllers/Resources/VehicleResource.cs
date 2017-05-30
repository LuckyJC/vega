using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using vega.Models;

namespace vega.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }

        //add foreign key property
        public int ModelId { get; set; }

        public bool IsRegistered { get; set; }
        
        public ContactResource ContactResource { get; set; }

        public ICollection<int> Features { get; set; }

        public VehicleResource()
        {
            Features = new Collection<int>();
        }
    }
}