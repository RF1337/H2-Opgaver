using DiningPhilosophers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ThreadDiningPhilisophers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating lists 
            List<Fork> forks = new List<Fork>();
            forks.Add(new Fork("Fork 1"));
            forks.Add(new Fork("Fork 2"));
            forks.Add(new Fork("Fork 3"));

            List<Philisopher> philisophers = new List<Philisopher>();
            philisophers.Add(new Philisopher("Benjamin", forks[0], forks[2]));
            philisophers.Add(new Philisopher("Tobias", forks[1], forks[0]));
            philisophers.Add(new Philisopher("Rasmus", forks[2], forks[1]));

            // Creating threads and starting them
            Thread thread = new Thread(philisophers[0].Switch);
            thread.Name = "Benjamin";
            thread.Start();

            Thread thread1 = new Thread(philisophers[1].Switch);
            thread1.Name = "Tobias";
            thread1.Start();

            Thread thread2 = new Thread(philisophers[2].Switch);
            thread2.Name = "Rasmus";
            thread2.Start();

            Console.Read();
        }
    }
}