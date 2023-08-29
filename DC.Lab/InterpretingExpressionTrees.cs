using System.Linq.Expressions;

namespace DC.Lab;

public class InterpretingExpressionTrees
{
    public static void Execute()
    {
        //DecomposeLambda();

        //DemoAdditionExpression();
        //UsingTheVisitorFactory();
        DeconstructingAComplexExpression();
    }

    static void DecomposeLambda()
    {
        Expression<Func<int, bool>> exprTree = num => num < 5;

        ParameterExpression param = (ParameterExpression)exprTree.Parameters[0];
        BinaryExpression operation = (BinaryExpression)exprTree.Body;
        ParameterExpression left = (ParameterExpression)operation.Left;
        ConstantExpression right = (ConstantExpression)operation.Right;

        Console.WriteLine($"Decomposed expression: {param.Name} => {left.Name} {operation.NodeType} {right.Value}");
    }

    static void DemoAdditionExpression()
    {
        Expression<Func<int, int, int>> addition = (a, b) => a + b;

        Console.WriteLine($"This expression is a {addition.NodeType} expression type");
        Console.WriteLine($"The name of the lambda is {((addition.Name == null) ? "<null>" : addition.Name)}");
        Console.WriteLine($"The return type is {addition.ReturnType.ToString()}");
        Console.WriteLine($"The expression has {addition.Parameters.Count} arguments. They are:");

        foreach (var argumentExpression in addition.Parameters)
            Console.WriteLine($"\tParameter Type: {argumentExpression.Type.ToString()}, Name: {argumentExpression.Name}");

        var additionBody = (BinaryExpression)addition.Body;

        Console.WriteLine($"The body is a {additionBody.NodeType} expression");
        Console.WriteLine($"The left side is a {additionBody.Left.NodeType} expression");

        var left = (ParameterExpression)additionBody.Left;

        Console.WriteLine($"\tParameter Type: {left.Type.ToString()}, Name: {left.Name}");
        Console.WriteLine($"The right side is a {additionBody.Right.NodeType} expression");

        var right = (ParameterExpression)additionBody.Right;

        Console.WriteLine($"\tParameter Type: {right.Type.ToString()}, Name: {right.Name}");
    }

    static void UsingTheVisitorFactory()
    {
        Expression<Func<int, int, int>> addition = (a, b) => a + b;
        var visitor = new LambdaVisitor(addition);
        visitor.Visit("");
    }

    static void DeconstructingAComplexExpression()
    {
        Expression<Func<int, int>> factorial = (n) =>
            n == 0 ?
                1 :
                Enumerable.Range(1, n)
                    .Aggregate((product, factor) =>
                        product + factor);

        var visitor = new LambdaVisitor(factorial);
        visitor.Visit("");
    }
}
