using LuggageSorting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.Controller
{
    internal class Counter
    {
        // count acts as an id to track the luggageNumber
        int id = 0;
        private Queue<Luggage> checkedInLuggage = new Queue<Luggage>();

        // Method for checking in
        /// <summary>
        /// Adds luggage to a list of checkedInLuggage
        /// </summary>
        /// <param name="flight"></param>
        public void CheckIn(Flight flight)
        {
            id++;
            Model.Luggage luggage = new Luggage(id);
            checkedInLuggage.Enqueue(luggage);
        }

        public void TransferLuggageToSorter(Sorter sorter)
        {
            foreach (Luggage luggage in checkedInLuggage)
            {
                sorter.SortLuggage(luggage);
                checkedInLuggage.Dequeue();
            }
        }
    }
}
