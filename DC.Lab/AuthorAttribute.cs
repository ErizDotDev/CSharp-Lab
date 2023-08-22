namespace DC.Lab;

[AttributeUsage(AttributeTargets.Class |
    AttributeTargets.Struct,
    AllowMultiple = true)]
public class AuthorAttribute : Attribute
{
    string Name;

    public double Version;

    public AuthorAttribute(string name)
    {
        Name = name;

        // Default value
        Version = 1.0;
    }

    public string GetName() => Name;
}
