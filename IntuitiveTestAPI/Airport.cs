using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntuitiveTestAPI
{
    public class Airport
    {
        [Key]
        public int AirportID { get; set; }

        [Column(TypeName = "char(3)")]
        [Required]
        public string IATACode { get;set; }

        public int GeographyLevel1ID { get; set; }

        public Country Country { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Type { get; set; }

        public ICollection<AirportInAirportGroup> AirportInAirportGroups { get; set; }

    }
}
