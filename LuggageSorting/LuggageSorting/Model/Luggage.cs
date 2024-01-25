using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.Model
{
    internal class Luggage
    {
        public int LuggageNumber { get; set; }

        public Luggage(int luggageNumber)
        {
            LuggageNumber = luggageNumber;
        }
    }
}
