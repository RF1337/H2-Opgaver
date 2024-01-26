using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.Model
{
    internal class Flight
    {
        public int FlightNumber { get; set; }
        public FD Destination { get; set; }

        public Queue<Luggage> LuggageQueue { get; set; }

        public enum FD
        {
            Copenhagen,
            Frankfurt,
            Paris,
            London, 
            Moscow
        }

        public Flight(int flightNumber, FD destination)
        {
            FlightNumber = flightNumber;
            Destination = destination;
            LuggageQueue = new Queue<Luggage>();
        }
    }
}
