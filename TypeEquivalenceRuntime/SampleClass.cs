using System;
using TypeEquivalenceInterface;

namespace TypeEquivalenceRuntime
{
    public class SampleClass : ISampleInterface
    {
        private string _userInput;
        public string UserInput { get { return _userInput; } }

        public void GetUserInput()
        {
            Console.WriteLine("Please enter a value:");
            _userInput = Console.ReadLine();
        }

        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
