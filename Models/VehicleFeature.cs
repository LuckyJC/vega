using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        //add foreign key properties and then navigation properties for the both vehicle and feature
        //use both vehicleId and featureId to create a composite primary key (adding another id field is not necessary) this will ensure that
        //a feature is not duplicated in a given vehicle; this combo of ids is the unique identifier- need to use fluent api to create composite key; see dbcontext
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }

    }
}