namespace DC.Lab;

public static class StringExtensions
{
    public static string ToString2<T>(this List<T> l)
    {
        var result = string.Empty;

        foreach (T item in l)
        {
            var prefix = string.IsNullOrEmpty(result) ? string.Empty : ", ";
            result += $"{prefix}{item}";
        }

        return string.IsNullOrEmpty(result)
            ? "{}"
            : $"{{{result}}}";
    }

    public static string ToString<T>(this List<T> l, string fmt)
    {
        var result = string.Empty;

        foreach (T item in l)
        {
            var ifmt = item as IFormattable;

            if (ifmt is not null)
            {
                var prefix = string.IsNullOrEmpty(result) ? string.Empty : ", ";
                result += $"{prefix}{ifmt?.ToString(fmt, null)}";
            }
            else
            {
                result += ToString2(l);
            }
        }

        return string.IsNullOrEmpty(result)
            ? "{}"
            : $"{{{result}}}";
    }
}
