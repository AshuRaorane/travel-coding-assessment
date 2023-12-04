using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntuitiveTestAPI
{
    public class AirportGroup
    {
        [Key]
        public int AirportGroupID { get;set; }

        [Required]
        [Column(TypeName="varchar(100)")]
        public string Name { get;set; } = string.Empty;

        public ICollection<AirportInAirportGroup> AirportInAirportGroups { get; set; }
    }
}
