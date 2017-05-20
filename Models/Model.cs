using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
    [Table("Models")]   
    public class Model
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Make Make { get; set; }
        //entity framework should know that Make and MakeId are the same; will create one column for both properties
        public int MakeId { get; set; }
    }
}