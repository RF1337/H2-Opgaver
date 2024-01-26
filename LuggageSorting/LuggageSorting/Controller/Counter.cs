using LuggageSorting.Model;
using LuggageSorting.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.Controller
{
    internal class Counter
    {
        Sorter sorter = new Sorter(GuiService.Instance);

        public Counter(Sorter sorter, GuiService guiService)
        {
            guiService = GuiService.Instance;
            this.sorter = sorter;
        }

        // Doesn't actually work, just to show how I would make it if there was an ability to open or close the counter
        bool hasCheckedIn = false;

        // Method for checking in
        /// <summary>
        /// Gives unsorted luggage to the sorter
        /// </summary>
        /// <param name="flight"></param>
        public void CheckInProducer(List<Luggage> luggageList)
        {
            while (true)
            {
                lock (sorter.unsortedLuggage)
                {
                    while (hasCheckedIn == true)
                    {
                        Monitor.Wait(sorter.unsortedLuggage);
                    }
                    foreach (Luggage luggage in luggageList)
                    {
                        sorter.unsortedLuggage.Enqueue(luggage);
                        GuiService.Instance.PrintMessage($"Luggage with ID: {luggage.LuggageId} checked in");
                    }
                    hasCheckedIn = true;
                    Monitor.Pulse(sorter.unsortedLuggage);
                }
            }
        }
    }
}
