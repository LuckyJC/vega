using System.Collections.Generic;
using System.Collections.ObjectModel;
using vega.Models;

namespace vega.Controllers.Resources
{
    public class MakeResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //could also use list instead of collection if wanted; list would allow us to use index
        public ICollection<ModelResource> Models { get; set; }

        //constructor to initialize Collection so I don't forget to do it later
        public MakeResource()
        {
            Models = new Collection<ModelResource>();
        }
    
    }
}