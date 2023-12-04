using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IntuitiveTestAPI
{
    public class RouteInputModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int DepartureAirportID { get;set; }

        public int ArrivalAirportID { get; set; }

        public int DepartureAirportGroupID { get; set; }

        public int ArrivalAirportGroupID { get; set; }

    }
}
