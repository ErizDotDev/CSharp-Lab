public class GenericList<T> : IEnumerable<T>
{
    protected class Node
    {
        public Node _next;

        private T _data;

        public Node(T t)
        {
            _next = null!;
            _data = t;
        }

        public Node Next
        {
            get => _next;
            set => _next = value;
        }

        public T Data
        {
            get => _data;
            set => _data = value;
        }
    }

    protected Node _head;
    protected Node _current = null!;

    public GenericList()
    {
        _head = null!;
    }

    public void AddHead(T t)
    {
        Node n = new Node(t);
        n.Next = _head;
        _head = n;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class SortedList<T> : GenericList<T> where T : System.IComparable<T>
{
    public void BubbleSort()
    {
        if (null == _head || null == _head.Next)
            return;

        bool swapped;

        do
        {
            Node previous = null!;
            Node current = _head;
            swapped = false;

            while (current.Next is not null)
            {
                if (current.Data.CompareTo(current._next.Data) > 0)
                {
                    Node tmp = current._next;
                    current._next = current._next._next;
                    tmp._next = current;

                    if (previous is null)
                        _head = tmp;
                    else
                        previous._next = tmp;

                    previous = tmp;
                    swapped = true;
                }
                else
                {
                    previous = current;
                    current = current._next;
                }
            }
        } while (swapped);
    }
}

public class Person : IComparable<Person>
{
    string _name;
    int _age;

    public Person(string s, int i)
    {
        _name = s;
        _age = i;
    }

    public int CompareTo(Person? p)
    {
        return _age - p!._age;
    }

    public override string ToString()
    {
        return $"{_name}:{_age}";
    }

    public bool Equals(Person? p)
    {
        return (this._age == p!._age);
    }
}

class Program
{
    static void Main()
    {
        SortedList<Person> list = new SortedList<Person>();

        string[] names =
        [
            "Franscoise",
            "Bill",
            "Li",
            "Sandra",
            "Gunnar",
            "Alok",
            "Hiroyuki",
            "Maria",
            "Alessandro",
            "Raul"
        ];

        int[] ages = [45, 19, 28, 23, 18, 9, 108, 72, 30, 35];

        for (int x = 0; x < 10; x++)
            list.AddHead(new Person(names[x], ages[x]));

        foreach (Person p in list)
            Console.WriteLine(p.ToString());

        Console.WriteLine("Done with unsorted list");

        list.BubbleSort();

        foreach (var p in list)
            Console.WriteLine(p.ToString());

        Console.WriteLine("Done with sorted list");
    }
}