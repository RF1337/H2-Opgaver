namespace ProducerConsumer5
{
    internal class Program
    {
        static Queue<int> products = new Queue<int>();
        object _lock = new object();

        public void Produce()
        {
            int item = 0;
            while (true)
            {
                lock (_lock)
                {
                    // While the product count is 3 or over, the thread should wait
                    while (products.Count >= 3)
                    {
                        Monitor.Wait(_lock);
                    }
                    // Letting it enqueue items until the while condition of 3 or over is reached
                    // Meaning that it will produce 3 items, then wait
                    products.Enqueue(item);
                    Console.WriteLine($"Producer har produceret: {products.Count}");
                    Monitor.Pulse(_lock);
                }
            }
        }

        public void Consume()
        {
            int count = 0;

            while (true)
            {
                lock (_lock)
                {
                    while (products.Count <= 0)
                    {
                        Monitor.Wait(_lock);
                    }
                    products.Dequeue();
                    count++;
                    Console.WriteLine($"Consumer has consumed: {count}");
                    Monitor.Pulse(_lock);

                    if (products.Count == 0)
                    {
                        count = 0;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Program pr = new Program();

            Thread producer = new Thread(pr.Produce);
            producer.Start();

            Thread consumer = new Thread(pr.Consume);
            consumer.Start();

            Console.Read();
        }
    }
}