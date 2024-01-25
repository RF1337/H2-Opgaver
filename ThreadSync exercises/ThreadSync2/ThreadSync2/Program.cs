namespace ThreadSync2
{
    internal class Program
    {
        static int SymbolCount = 0;
        object _lock = new object();

        // Method for printing *
        public void PrintStars()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    for (int i = 0; i < 60; i++)
                    {
                        Console.Write("*");
                        SymbolCount++;
                        Thread.Sleep(1);
                    }
                    Console.Write(SymbolCount);
                    Console.WriteLine();
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }

        public void PrintHashtags()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    for (int i = 0; i < 60; i++)
                    {
                        Console.Write("#");
                        SymbolCount++;
                        Thread.Sleep(1);
                    }
                    Console.Write(SymbolCount);
                    Console.WriteLine();
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }

        static void Main(string[] args)
        {
            Program pr = new Program();

            Thread thread = new Thread(pr.PrintStars);
            Thread thread1 = new Thread(pr.PrintHashtags);

            thread.Start();
            thread1.Start();
        }
    }
}