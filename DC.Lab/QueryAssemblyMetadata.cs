using System.Reflection;

namespace DC.Lab;

public class QueryAssemblyMetadata
{
    public static void Execute()
    {
        var assembly = Assembly.Load(
            "System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");

        var pubTypesQuery = from type in assembly.GetTypes()
                            where type.IsPublic
                            from method in type.GetMethods()
                            where method.ReturnType.IsArray == true
                                || (method.ReturnType.GetInterface(
                                        typeof(IEnumerable<>).FullName!) != null
                                && method.ReturnType.FullName != "System.String")
                            group method.ToString() by type.ToString();

        foreach (var groupOfMethods in pubTypesQuery)
        {
            Console.WriteLine($"Type: {groupOfMethods.Key}");

            foreach (var method in groupOfMethods)
                Console.WriteLine($"\t{method}");
        }
    }
}
