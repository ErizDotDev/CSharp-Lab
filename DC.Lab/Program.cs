using DC.Lab;
using System.Reflection;

Type dateType = typeof(DateTime);
PropertyInfo prop = dateType.GetProperty("Now");
var (isStatic, isRO, isIndexed, propType) = prop;

Console.WriteLine($"\n The {dateType.FullName}.{prop.Name} property:");
Console.WriteLine($"    PropertyType:   {propType.Name}");
Console.WriteLine($"    Static:         {isStatic}");
Console.WriteLine($"    Read-only:      {isRO}");
Console.WriteLine($"    Indexed:        {isIndexed}");

Type listType = typeof(List<>);
prop = listType.GetProperty("Item",
    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
var (hasGetAndSet, sameAccess, accessibility, getAccessibility, setAccessibility) = prop;
Console.WriteLine($"\nAccessibility of the {listType.FullName}.{prop.Name} property: ");

if (!hasGetAndSet | sameAccess)
{
    Console.WriteLine(accessibility);
}
else
{
    Console.WriteLine($"\n The get accessor: {getAccessibility}");
    Console.WriteLine($"The set accessor: {setAccessibility}");
}