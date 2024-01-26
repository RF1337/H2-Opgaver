using LuggageSorting.Model;
using LuggageSorting.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.Controller
{
    internal class Sorter
    {
        public Queue<Luggage> unsortedLuggage = new Queue<Luggage>();

        public Sorter(GuiService guiService)
        {
            guiService = GuiService.Instance;
        }

        /// <summary>
        /// Takes luggage and assigns it to it's respective flight from the flights list
        /// </summary>
        /// <param name="flights"></param>
        public void AssignLuggageToFlightProducerConsumer(List<Flight> flights)
        {
            while (true)
            {
                lock (unsortedLuggage)
                {
                    while (unsortedLuggage.Count == 0)
                    {
                        Monitor.Wait(unsortedLuggage);
                    }
                }
                Luggage luggage = unsortedLuggage.Dequeue();
                foreach (Flight flight in flights)
                {
                    if (flight.FlightNumber == luggage.FlightNumber)
                    {
                        flight.LuggageQueue.Enqueue(luggage);
                        GuiService.Instance.PrintMessage($"Luggage with ID: {luggage.LuggageId} assigned to flight {flight.FlightNumber} and is on it's way to {flight.Destination}");
                        break;
                    }
                }
            }
        }
    }
}
