namespace Ovelse3
{
    internal class Program
    {
        Random r = new Random();

        static byte WrongNumber = 0;
      
        public void GenerateTemperature()
        {
            
            while (WrongNumber < 3)
            {
               int randonNumber = r.Next(-20, 120);
                Console.WriteLine($"{randonNumber}");
                Thread.Sleep(100);

                if (randonNumber > 100 || randonNumber < 0)
                {
                    WrongNumber++;
                }
            }
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            Thread thread = new Thread(pr.GenerateTemperature);

            // If it has printed wrong number 3 times then stop thread
           
            thread.Start();

            Console.Read();
        }
    }
}