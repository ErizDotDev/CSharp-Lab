namespace DC.Lab;

internal class CList<T> : List<T>
{
    public CList(IEnumerable<T> collection) : base(collection) { }

    public CList() : base() { }

    public override string ToString()
    {
        var result = string.Empty;

        foreach (T item in this)
        {
            if (string.IsNullOrEmpty(result))
                result += item?.ToString();
            else
                result += $", {item}";
        }

        return result;
    }
}
