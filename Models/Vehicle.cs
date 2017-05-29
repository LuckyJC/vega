using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }

        //add foreign key property
        public int ModelId { get; set; }

        //add navigation property
        public Model Model { get; set; }

        public bool IsRegistered { get; set; }
        
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        
        [StringLength(255)]        
        public string ContactEmail { get; set; }
        
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
        
        public DateTime LastUpdate { get; set; }

        public ICollection<VehicleFeature> Features { get; set; }

        //best practice to set a constructor to initialize features
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
        }
    }
}