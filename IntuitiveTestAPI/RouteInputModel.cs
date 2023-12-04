using System.ComponentModel.DataAnnotations;
namespace IntuitiveTestAPI
{
    public class RouteInputModel
    {
        public int Id { get; set; }
        public int DepartureAirportID { get;set; }

        public int ArrivalAirportID { get; set; }

        public int DepartureAirportGroupID { get; set; }

        public int ArrivalAirportGroupID { get; set; }

    }
}
