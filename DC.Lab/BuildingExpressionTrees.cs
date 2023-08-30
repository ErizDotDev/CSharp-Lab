using System.Linq.Expressions;

namespace DC.Lab;

public class BuildingExpressionTrees
{
    public static void Execute()
    {
        CreatingCodeBlocks();
    }

    static void CreatingNodes()
    {
        // Multiple statements
        // Creating the constants
        var one = Expression.Constant(1, typeof(int));
        var two = Expression.Constant(2, typeof(int));

        // Creating the addition expression
        var addition = Expression.Add(one, two);

        // Creating the lambda
        var lambda = Expression.Lambda(addition);

        //Simplified way
        var lambda2 = Expression.Lambda(
            Expression.Add(
                Expression.Constant(1, typeof(int)),
                Expression.Constant(2, typeof(int))
            )
        );
    }

    static void CreatingComplexTrees()
    {
        // This is the expression to be created
        Expression<Func<double, double, double>> distanceCalc =
            (x, y) => Math.Sqrt(x * x + y * y);

        var xParameter = Expression.Parameter(typeof(double), "x");
        var yParameter = Expression.Parameter(typeof(double), "y");

        var xSquared = Expression.Multiply(xParameter, xParameter);
        var ySquared = Expression.Multiply(yParameter, yParameter);
        var sum = Expression.Add(xSquared, ySquared);

        var sqrtMethod = typeof(Math).GetMethod("Sqrt", new[] { typeof(double) }) ?? throw new InvalidOperationException("Math.Sqrt not found!");
        var distance = Expression.Call(sqrtMethod);

        var distanceLambda = Expression.Lambda(
            distance,
            xParameter,
            yParameter
        );
    }

    static void CreatingCodeBlocks()
    {
        // This is the delegate to be created
        Func<int, int> factorialFunc = (n) =>
        {
            var res = 1;

            while (n > 1)
            {
                res = res * n;
                n--;
            }

            return res;
        };

        var nArgument = Expression.Parameter(typeof(int), "n");
        var result = Expression.Variable(typeof(int), "result");

        // Creating a label that represents the return value
        LabelTarget label = Expression.Label(typeof(int));

        var initializeResult = Expression.Assign(result, Expression.Constant(1));

        // This is the inner block that performs the multiplication,
        // and decrements the value of 'n'
        var block = Expression.Block(
            Expression.Assign(result,
                Expression.Multiply(result, nArgument)),
            Expression.PostDecrementAssign(nArgument)
        );

        // Creating the method body
        BlockExpression body = Expression.Block(
            new[] { result },
            initializeResult,
            Expression.Loop(
                Expression.IfThenElse(
                    Expression.GreaterThan(nArgument, Expression.Constant(1)),
                    block,
                    Expression.Break(label, result)
                ),
                label
            )
        );

        int factorial = Expression.Lambda<Func<int, int>>(body, nArgument).Compile()(5);

        Console.WriteLine(factorial);
    }
}
