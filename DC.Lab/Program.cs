using System.Globalization;

namespace DC.Lab;

class Program
{
    public static void Main()
    {
        string[] cultureNames = { string.Empty, "en-US", "fr-FR", "ru-RU" };

        foreach (var cultureName in cultureNames)
        {
            var boolValue = true;
            var culture = CultureInfo.CreateSpecificCulture(cultureName);
            var formatter = new BooleanFormatter(culture);

            string result = string.Format(formatter, "Value or '{0}: {1}",
                culture.Name, boolValue);
            Console.WriteLine(result);
        }
    }
}

public class BooleanFormatter : ICustomFormatter, IFormatProvider
{
    private CultureInfo _culture;

    public BooleanFormatter() : this(CultureInfo.CurrentCulture) { }

    public BooleanFormatter(CultureInfo culture)
    {
        _culture = culture;
    }

    public object? GetFormat(Type? formatType) =>
        formatType == typeof(ICustomFormatter)
            ? this
            : null;

    public string Format(string? fmt, object? arg, IFormatProvider? formatProvider)
    {
        if (!formatProvider!.Equals(this))
            return null!;

        if (arg is not Boolean)
            return null!;

        bool value = (bool)arg;

        return _culture.Name switch
        {
            "en-US" => value.ToString(),
            "fr-FR" => value ? "vrai" : "faux",
            "ru-RU" => value ? "верно" : "неверно",
            _ => value.ToString()
        };
    }
}