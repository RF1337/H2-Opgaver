using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleMachine
{
    internal class Bottle
    {
        public string Type { get; set; } = "";
        public int Id { get; set; }

        public Bottle(string type, int id)
        {
            Type = type;
            Id = id;
        }
    }
}
