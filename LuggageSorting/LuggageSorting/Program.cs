using LuggageSorting.Controller;

namespace LuggageSorting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();
            Sorter sorter = new Sorter();

            //Thread checkInThread = new Thread(counter.CheckIn);
            //checkInThread.Start();

            //Thread sortingThread = new Thread(sorter.SortLuggage);
            //sortingThread.Start();
        }
    }
}