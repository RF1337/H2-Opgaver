namespace ProducerConsumer4
{
    internal class Program
    {
        static Queue<int> products = new Queue<int>();

        public void Produce()
        {
            int count = 0;
            while (true)
            {
                Thread.Sleep(1000);
                while (products.Count < 3)
                {
                    count++;
                    products.Enqueue(count);
                    Console.WriteLine("Producer har produceret: " + products.Count);
                }
                if (products.Count == 3)
                {
                    Console.WriteLine("Kan ikke producere mere");
                }
            }
        }

        public void Consume()
        {
            while (true)
            {
                Thread.Sleep(1000);
                while (products.Count > 0)
                {
                    Console.WriteLine("Consumer har consumet " + products.Count);
                    products.Dequeue();
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