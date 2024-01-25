using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    internal class Fork
    {
        public string Name { get; set; }
        public Fork(string name)
        {
            Name = name;
        }
    }
}
