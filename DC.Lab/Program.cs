namespace DC.Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            RunArraysAndVectorsDemo();
            RunDictionaryDemo(args);
        }

        static void RunArraysAndVectorsDemo()
        {
            // Create a store:
            var store = new DataSamples(500000);

            // Access:
            var measure = store[3];
            Console.WriteLine(measure.HiTemp);

            // Update:
            store[80] = measure;
            Console.WriteLine(store[80].HiTemp);

            // Load a new page:
            var measure2 = store[2020];
            Console.WriteLine(measure2.LoTemp);

            // Load another new page
            store[3547] = measure2;
            Console.WriteLine(store[3547].LoTemp);

            // Make sure pages rotate in and out of memory:
            for (int j = 5; j < 500000; j += 1100)
            {
                var item = store[j];
                Console.WriteLine(item.AirPressure);
            }

            // Check for valid arguments
            try
            {
                var item = store[2000000];
                Console.WriteLine("Didn't check bounds");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Exception encountered");
            }
        }

        static void RunDictionaryDemo(string[] args)
        {
            var argThings = new ArgsAction();
            argThings.SetOption("--a", () => Console.WriteLine("-a option selected"));
            argThings.SetOption("--e", () => Console.WriteLine("-e option selected"));

            var processor = new ArgsProcessor(argThings);
            processor.Process(args);
        }
    }
}