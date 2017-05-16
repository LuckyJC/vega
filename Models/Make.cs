using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace vega.Models
{
    public class Make
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //could also use list instead of collection if wanted; list would allow us to use index
        public ICollection<Model> Models { get; set; }

        //constructor to initialize Collection so I don't forget to do it later
        public Make()
        {
            Models = new Collection<Model>();
        }
    }
}