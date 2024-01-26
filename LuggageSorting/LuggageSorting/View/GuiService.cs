using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorting.View
{
    internal class GuiService
    {
        private static GuiService _instance;

        private GuiService() { }

        public static GuiService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GuiService();
                }
                return _instance;
            }
        }
        public void PrintMessage(string message)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} - {message}");
        }
    }
}