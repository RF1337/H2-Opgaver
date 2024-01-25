namespace Ovelse1og2
{
    internal class Program
    {
        public void PrintToConsole1()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("C#-trådning er nemt");
                Thread.Sleep(1000);
            }
        }

        public void PrintToConsole2()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Også med flere tråde");
                Thread.Sleep(1000);
            }
        }

        class threprog
        {
            static void Main(string[] args)
            {
                Program pg = new Program();
                
                Thread thread = new Thread(pg.PrintToConsole1);
                Thread thread2 = new Thread(pg.PrintToConsole2);
                thread.Start();
                thread2.Start();
            }
        }
    }
}