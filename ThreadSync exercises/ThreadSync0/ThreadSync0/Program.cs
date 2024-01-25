using System.Diagnostics.CodeAnalysis;

namespace ThreadSync0
{
    internal class Program
    {
        static int sum = 0;

        static void Main(string[] args)
        {
            // Creating threads 
            Thread[] threads = new Thread[1000];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(AddOne);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine(sum);
        }
        static void AddOne()
        {
            //sum++;
            Interlocked.Increment(ref sum);
        }
    }
}