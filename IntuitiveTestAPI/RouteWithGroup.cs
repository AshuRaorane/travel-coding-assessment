using System.ComponentModel.DataAnnotations;
namespace IntuitiveTestAPI
{
    public class RouteWithGroup
    {
        [Key]
        public int RouteWithGroupID { get; set; }

        [Required]
        public int DepartureAirportGroupID { get; set; }

        [Required]
        public int ArrivalAirportGroupID { get; set; }

        // Navigation properties
        public AirportGroup DepartureAirportGroup { get; set; }
        public AirportGroup ArrivalAirportGroup { get; set; }

    }
}
