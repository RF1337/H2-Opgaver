namespace ThreadSync1
{
    internal class Program
    {
        static int count = 0;
        object _lock = new object();

        public void CountUp()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    count += 2;
                    Console.Write(count + " ");
                    Thread.Sleep(1000);
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }

        public void CountDown()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    count--;
                    Console.Write(count + " ");
                    Thread.Sleep(1000);
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

            Thread thread = new Thread(pr.CountUp);
            Thread thread1 = new Thread(pr.CountDown);

            thread.Start();
            Thread.Sleep(1);
            thread1.Start();
        }
    }
}