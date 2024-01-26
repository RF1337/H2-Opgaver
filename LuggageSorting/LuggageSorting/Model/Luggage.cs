using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.Model
{
    internal class Luggage
    {
        public int LuggageId { get; set; }
        public int FlightNumber { get; set; }

        public Luggage(int luggageId, int flightNumber)
        {
            LuggageId = luggageId;
            FlightNumber = flightNumber;
        }
    }
}
