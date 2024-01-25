namespace BottleMachine
{
    internal class Program
    {
        // Buffer 1, bottles as a whole
        static Queue<Bottle> bottles = new Queue<Bottle>();

        // Buffer 2, bottles seperated into beer
        static Queue<Bottle> beerBottles = new Queue<Bottle>();

        // Buffer 3, bottles seperated into soda
        static Queue<Bottle> sodaBottles = new Queue<Bottle>();

        object _lock = new object();

        // Int for keeping track of bottle's id
        static int id = 0;

        static void Main()
        {
            Program pr = new Program();

            Thread producer = new Thread(pr.Produce);
            producer.Start();

            Thread splitterConsumer = new Thread(pr.Split);
            splitterConsumer.Start();
            splitterConsumer.Name = "Splitter";

            Thread beerConsumer = new Thread(pr.ConsumeBeer);
            beerConsumer.Start();
            beerConsumer.Name = "Beer consumer";

            Thread sodaConsumer = new Thread(pr.ConsumeSoda);
            sodaConsumer.Start();
            sodaConsumer.Name = "Soda consumer";
        }

        // Method for putting them into first buffer
        public void Produce()
        {
            // Random object used for determening amound of time to put a bottle into machine & type of bottle
            Random r = new Random();

            // While true loop to ensure it runs at all times
            while (true)
            {
                // Locking the bottles queue
                lock (bottles)
                {
                    while (bottles.Count > 5)
                    {
                        // Putting the thread in a waiting state until it gets pulsed
                        Monitor.Wait(bottles);
                    }
                    // Incrementing id variable, so the id of each bottle can be tracked
                    id++;
                    // Takes a random amount of ms for a bottle to be put into machine
                    Thread.Sleep(r.Next(0, 1000));
                    if (r.Next(1,3) == 1)
                    {
                        // Enqueuing the bottle into the bottles queue
                        bottles.Enqueue(new Bottle("beer", id));
                    }
                    else
                    {
                        bottles.Enqueue(new Bottle("soda", id));
                    }
                    // Notifies waiting threads that they can continue
                    Monitor.Pulse(bottles);
                    Console.WriteLine($"Putting bottle: {id} into machine");
                }
            }
        }

        // Splitter method to split the bottles received from buffer into their respective queues
        public void Split()
        {
            while (true)
            {
                lock (bottles)
                {
                    while (bottles.Count <= 0)
                    {
                        Monitor.Wait(bottles);
                    }

                    // Setting bottle variable to bottle queue's dequeue method, so I can check what type it dequeues
                    Bottle bottle = bottles.Dequeue();

                    // Checking if the type is beer
                    if (bottle.Type == "beer")
                    {
                        // Putting a lock on beer queue
                        lock (beerBottles)
                        {
                            // Enqueuing to bottles queue
                            beerBottles.Enqueue(bottle);
                            Console.WriteLine($"Adding bottle {bottle.Id} to beer queue");
                            // Notifies waiting threads that they can continue
                            Monitor.Pulse(beerBottles);
                        }
                    }

                    // Checking if the type is soda
                    else if (bottle.Type == "soda")
                    {
                        // Putting a lock on soda queue
                        lock (sodaBottles)
                        {
                            sodaBottles.Enqueue(bottle);
                            Console.WriteLine($"Adding bottle {bottle.Id} to soda queue");
                            Monitor.Pulse(sodaBottles);
                        }
                    }
                    else
                    {
                        // Error handling
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bottle type does not exist");
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        // Method for dequeuing the beer bottles from their queue
        public void ConsumeBeer()
        {
            while (true)
            {
                lock (beerBottles)
                {
                    while (beerBottles.Count == 0)
                    {
                        Monitor.Wait(beerBottles);
                    }
                    Bottle bottle = beerBottles.Dequeue();
                    Console.WriteLine($"{Thread.CurrentThread.Name} has consumed type: {bottle.Type}, id: {bottle.Id}");
                    Monitor.Pulse(beerBottles);
                }
            }
        }

        // Method for dequeuing the soda bottles from their queue
        public void ConsumeSoda()
        {
            while (true)
            {
                lock (sodaBottles)
                {
                    while (sodaBottles.Count == 0)
                    {
                        Monitor.Wait(sodaBottles);
                    }
                    Bottle bottle = sodaBottles.Dequeue();
                    Monitor.Pulse(sodaBottles);
                    Console.WriteLine($"{Thread.CurrentThread.Name} has consumed type: {bottle.Type}, id: {bottle.Id}");
                }
            }
        }
    }
}