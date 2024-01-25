namespace Ovelse4
{
    internal class Program
    {

        // Creating variable to hold user input, the exercise says to put * as start input
        private char _key = '*';

        // Method for reading the key
        public void ReadKey()
        {
        // Keep listening
            while (true)
            {
                // Capture key input
                _key = Console.ReadKey().KeyChar;
            }
        }

        public void PrintKey()
        {
        // Keep listening
            while (true)
            {
                // Output key to console
                Console.Write(_key);

                // Sleep for 1ms after every character so it doesn't go too fast
                Thread.Sleep(1);
            }
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            Thread reader = new Thread(pr.ReadKey);
            reader.Start();

            Thread printer = new Thread(pr.PrintKey);
            printer.Start();
        }
    }
}