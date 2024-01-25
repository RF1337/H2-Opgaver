using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    internal class Philisopher
    {
        Random r = new Random();
        object _lock = new object();

        public string Name { get; set; } = "";
        public Fork LeftHand { get; set; }
        public Fork RightHand { get; set; }
        public state State { get; set; } = state.Thinking;

        public enum state
        {
            Eating,
            Thinking,
            Waiting
        }

        public void Switch()
        {
            while (true)
            {
                switch (State)
                {
                    case state.Thinking:
                        Console.WriteLine(Thread.CurrentThread.Name + " is now thinking");
                        Thread.Sleep(r.Next(1000, 4000));
                        Console.WriteLine(Thread.CurrentThread.Name + " is done thinking and is now waiting");
                        State = state.Waiting;
                        break;
                    case state.Waiting:
                        GetFork();
                        break;
                    case state.Eating:
                        Eat();
                        State = state.Thinking;
                        break;
                    default:
                        break;
                }
            }
        }

        public void GetFork()
        {
            if (Monitor.TryEnter(RightHand))
            {
                if (Monitor.TryEnter(LeftHand))
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " picked up forks");
                    State = state.Eating;
                    Monitor.Exit(RightHand);
                    Monitor.Exit(LeftHand);
                    return;
                }
            }
            return;
        }

        public void Eat()
        {
            // Is missing lock to make sure people don't eat at same time
            if (Monitor.TryEnter(RightHand))
            {
                if (Monitor.TryEnter(LeftHand))
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " is now eating");
                    Thread.Sleep(r.Next(5000, 15000));
                    Console.WriteLine(Thread.CurrentThread.Name + " is done eating");
                }
            }
        }

        public Philisopher(string name, Fork leftHand, Fork rightHand)
        {
            this.Name = name;
            this.LeftHand = leftHand;
            this.RightHand = rightHand;
        }
    }
}
