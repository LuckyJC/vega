using System.Collections.Generic;
using System.Collections.ObjectModel;
using vega.Core.Models;

namespace vega.Controllers.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        //could also use list instead of collection if wanted; list would allow us to use index
        public ICollection<KeyValuePairResource> Models { get; set; }

        //constructor to initialize Collection so I don't forget to do it later
        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    
    }
}