using LuggageSorting.Controller;
using LuggageSorting.Model;
using LuggageSorting.View;

namespace LuggageSorting
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Random r = new Random();
            int count = 0;

            List<Luggage> luggageList = new List<Luggage>();
            for (int i = 0; i < 100; i++)
            {
                count++;
                luggageList.Add(new Luggage(count, r.Next(1,5)));
            }
            List<Flight> flights = new List<Flight>();
            flights.Add(new Flight(1, Flight.FD.Copenhagen));
            flights.Add(new Flight(2, Flight.FD.Frankfurt));
            flights.Add(new Flight(3, Flight.FD.Paris));
            flights.Add(new Flight(4, Flight.FD.London) );
            flights.Add(new Flight(5, Flight.FD.Moscow));

            Sorter sorter = new Sorter(GuiService.Instance);
            Counter counter = new Counter(sorter, GuiService.Instance);

            Thread counterThread = new Thread(() => counter.CheckInProducer(luggageList));
            counterThread.Name = "Counter";
            counterThread.Start();

            Thread sorterThread = new Thread(() => sorter.AssignLuggageToFlightProducerConsumer(flights));
            sorterThread.Name = "Sorter";
            sorterThread.Start();

            Console.Read();
        }
    }
}