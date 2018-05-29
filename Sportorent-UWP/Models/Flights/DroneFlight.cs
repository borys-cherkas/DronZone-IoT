using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronZone_IoT.Models.Flights
{
    public class DroneFlight
    {
        public string Id { get; set; }

        public string DroneId { get; set; }

        public DateTime StartedDateTime { get; set; }
    }
}
