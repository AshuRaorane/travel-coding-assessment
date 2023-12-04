using System.ComponentModel.DataAnnotations;
namespace IntuitiveTestAPI
{
    public class Route
    {
        [Key]
        public int RouteID { get;set; }

        [Required]
        public int DepartureAirportID { get;set; }

        [Required]
        public int ArrivalAirportID { get; set; }

        // Navigation properties
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }

    }
}
