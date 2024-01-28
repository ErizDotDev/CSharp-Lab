using System;
using System.Reflection;
using TypeEquivalenceInterface;

namespace TypeEquivalenceClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Assembly sampleAssembly = Assembly.Load("TypeEquivalenceRuntime");

            ISampleInterface sampleClass =
                (ISampleInterface)sampleAssembly.CreateInstance("TypeEquivalenceRuntime.SampleClass");
            sampleClass.GetUserInput();

            Console.WriteLine(sampleClass.UserInput);
            Console.WriteLine(sampleAssembly.GetName().Version.ToString());

            Console.ReadLine();
        }
    }
}
