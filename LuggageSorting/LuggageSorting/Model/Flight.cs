using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.Model
{
    internal class Flight
    {
        public DateTime DepartureTime = new DateTime();
        public int FlightNumber { get; set; }
        public string Destination { get; set; } = "";

        public Queue<Luggage> LuggageList { get; set; }

        public Flight(DateTime departureTime, int flightNumber, string destination)
        {
            DepartureTime = departureTime;
            FlightNumber = flightNumber;
            Destination = destination;
            LuggageList = new Queue<Luggage>();
        }
    }
}
