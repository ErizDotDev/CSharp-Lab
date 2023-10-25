using System.Collections;

namespace DC.Lab;

public class DictionaryExample
{
    class RudimentaryMultiValuedDictionary<TKey, TValue> :
        IEnumerable<KeyValuePair<TKey, List<TValue>>> where TKey : notnull
    {
        private Dictionary<TKey, List<TValue>> internalDictionary = new();

        public IEnumerator<KeyValuePair<TKey, List<TValue>>> GetEnumerator() =>
            internalDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => internalDictionary.GetEnumerator();

        public List<TValue> this[TKey key]
        {
            get => internalDictionary[key];
            set => Add(key, value);
        }

        public void Add(TKey key, params TValue[] values) => Add(key, (IEnumerable<TValue>)values);

        public void Add(TKey key, IEnumerable<TValue> values)
        {
            if (!internalDictionary.TryGetValue(key, out List<TValue>? storedValues))
                internalDictionary.Add(key, storedValues = new List<TValue>());

            storedValues.AddRange(values);
        }
    }

    public static void Main()
    {
        RudimentaryMultiValuedDictionary<string, string> rudimentaryMultiValuedDictionary1 =
            new RudimentaryMultiValuedDictionary<string, string>()
            {
                { "Group1", "Bob", "John", "Mary" },
                { "Group2", "Eric", "Emily", "Debbie", "Jesse" }
            };

        RudimentaryMultiValuedDictionary<string, string> rudimentaryMultiValuedDictionary2 =
            new RudimentaryMultiValuedDictionary<string, string>()
            {
                ["Group1"] = new List<string>() { "Bob", "John", "Mary" },
                ["Group2"] = new List<string>() { "Eric", "Emily", "Debbie", "Jesse" }
            };

        RudimentaryMultiValuedDictionary<string, string> rudimentaryMultiValuedDictionary3 =
            new RudimentaryMultiValuedDictionary<string, string>()
            {
                ["Group1"] = new List<string>() { "Bob", "John", "Mary" },
                ["Group2"] = new List<string>() { "Eric", "Emily", "Debbie", "Jesse" }
            };

        Console.WriteLine("Using the first multi-valued dictionary created with a collection initializer");

        foreach (var group in rudimentaryMultiValuedDictionary1)
        {
            Console.WriteLine($"\r\nMembers of group {group.Key}: ");

            foreach (var member in group.Value)
                Console.WriteLine(member);
        }

        Console.WriteLine("\r\nUsing the second multi-valued dictionary created with a collection initializer");

        foreach (var group in rudimentaryMultiValuedDictionary2)
        {
            Console.WriteLine($"\r\nMembers of group {group.Key}: ");

            foreach (var member in group.Value)
                Console.WriteLine(member);
        }

        Console.WriteLine("\r\nUsing the third multi-valued dictionary created with a collection initializer");

        foreach (var group in rudimentaryMultiValuedDictionary3)
        {
            Console.WriteLine($"\r\nMembers of group {group.Key}: ");

            foreach (var member in group.Value)
                Console.WriteLine(member);
        }
    }
}