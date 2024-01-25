using LuggageSorting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.Controller
{
    internal class Sorter
    {
        private Queue<Luggage> sortedLuggage = new Queue<Luggage>();
        
        /// <summary>
        /// Adds unsorted luggage to a queue of sortedLuggage
        /// </summary>
        /// <param name="luggage"></param>
        public void SortLuggage(Luggage luggage)
        {
            // Maybe do it based on flight details
            sortedLuggage.Enqueue(luggage);
        }

        public void AssignLuggageToFlight(Flight flight, Luggage luggage)
        {
            // Check if belongs to flight and then add it to the queue
            flight.LuggageQueue.Enqueue(luggage);

            // Removing the luggage from sorted luggage list after it has been added to the flight's luggage list
            sortedLuggage.Dequeue();
        }
    }
}
