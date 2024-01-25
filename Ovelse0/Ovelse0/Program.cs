namespace Ovelse0
{
    internal class Program
    {
        // Creating method
        public void WorkThreadFunction()
        {
            // Creating loop that runs 5 times
            for (int i = 0; i < 5; i++)
            {
                // Writing "Simple thread" to console as many times as the loop asks for
                Console.WriteLine("Simple thread");
                // Writing what thread is currently being used
                Console.WriteLine($"{Thread.CurrentThread.Name}");
            }
        }

        class threprog
        {
            public static void Main()
            {
                // Creating new Program object
                Program pg = new Program();

                // Creating threads and setting it to run WorkThreadFunction method when started
                Thread thread = new Thread(new ThreadStart(pg.WorkThreadFunction));
                Thread thread2 = new Thread(new ThreadStart(pg.WorkThreadFunction));
                thread.Name = "Thread 1";
                thread2.Name = "Thread 2";

                // Starting threads
                thread.Start();
                thread2.Start();

                // Making sure program doesn't stop until user presses key
                Console.Read();
            }
        }
    }
}