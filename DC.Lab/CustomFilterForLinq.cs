using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace DC.Lab;

public class CustomFilterForLinq
{
    // Usage:
    // var qry = TextFilter(
    //      new List<Person>().AsQueryable(),
    //      "abcd"
    // )
    // .Where(x => x.DateOfBirth < new DateTime(2001, 1, 1));
    public static IQueryable<T> TextFilter<T>(IQueryable<T> source, string term)
    {
        if (string.IsNullOrEmpty(term))
            return source;

        // T is a compile-time placeholder for the element type of the query
        var elementType = typeof(T);

        // Get all string properties on this specific type.
        var stringProperties =
            elementType.GetProperties()
                .Where(x => x.PropertyType == typeof(string))
                .ToArray();

        if (!stringProperties.Any())
            return source;

        // Get the right overload of String.Contains
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        // Create a parameter for the expression tree:
        // the 'x' in 'x => x.PropertyName.Contains("term")'
        // The type of this parameter is the query's element type
        var prm = Parameter(elementType);

        // Map each property to an expression tree node
        var expressions = stringProperties
            .Select(prp =>
            // For each property, we have to construct an expression tree
            // like x.PropertyName.Contains("term")
            Call(
                Property(
                    prm,
                    prp
                ),
                containsMethod!,
                Constant(term)
            )
         ) as IEnumerable<Expression>;

        // Combine all the resultant expression nodes using ||
        var body = expressions
            .Aggregate(
                (prev, current) => Or(prev, current)
            );

        // Wrap the expression body in a compile-time typed lambda expression
        var lambda = Lambda<Func<T, bool>>(body, prm);

        // Because lambda is compile-time typed (albeit with a generic parameter)
        // we can use it with the Where method.
        return source.Where(lambda);
    }
}
