using System.Linq.Expressions;

namespace DC.Lab;

public abstract class Visitor
{
    private readonly Expression node;

    protected Visitor(Expression node) => this.node = node;

    public abstract void Visit(string prefix);

    public ExpressionType NodeType => node.NodeType;

    public static Visitor CreateFromExpression(Expression node) =>
        node.NodeType switch
        {
            ExpressionType.Constant => new ConstantVisitor((ConstantExpression)node),
            ExpressionType.Lambda => new LambdaVisitor((LambdaExpression)node),
            ExpressionType.Parameter => new ParameterVisitor((ParameterExpression)node),
            ExpressionType.Add => new BinaryVisitor((BinaryExpression)node),
            ExpressionType.Equal => new BinaryVisitor((BinaryExpression)node),
            ExpressionType.Multiply => new BinaryVisitor((BinaryExpression)node),
            ExpressionType.Conditional => new ConditionalVisitor((ConditionalExpression)node),
            ExpressionType.Call => new MethodCallVisitor((MethodCallExpression)node),
            _ => throw new NotImplementedException($"Node not processed yet: {node.NodeType}")
        };
}

// Lambda visitor
public class LambdaVisitor : Visitor
{
    private readonly LambdaExpression node;

    public LambdaVisitor(LambdaExpression node) : base(node) => this.node = node;

    public override void Visit(string prefix)
    {
        Console.WriteLine($"{prefix}This expression is a {NodeType} expression type.");
        Console.WriteLine($"{prefix}The name of the lambda is {((node.Name == null) ? "<null>" : node.Name)}");
        Console.WriteLine($"{prefix}The return type is {node.ReturnType}");
        Console.WriteLine($"{prefix}The expression has {node.Parameters.Count} argument(s). They are:");

        // Visit each parameter
        foreach (var argumentExpression in node.Parameters)
        {
            var argumentVisitor = CreateFromExpression(argumentExpression);
            argumentVisitor.Visit(prefix + "\t");
        }

        Console.WriteLine($"{prefix}The expression body is:");

        // Visit the body
        var bodyVisitor = CreateFromExpression(node.Body);
        bodyVisitor.Visit(prefix + "\t");
    }
}

// Binary visitor
public class BinaryVisitor : Visitor
{
    private readonly BinaryExpression node;

    public BinaryVisitor(BinaryExpression node) : base(node) => this.node = node;

    public override void Visit(string prefix)
    {
        Console.WriteLine($"{prefix}This binary expression is a {NodeType} expression");

        var left = CreateFromExpression(node.Left);
        Console.WriteLine($"{prefix}The left argument is:");
        left.Visit(prefix + "\t");

        var right = CreateFromExpression(node.Right);
        Console.WriteLine($"{prefix} The right argument is:");
        right.Visit(prefix + "\t");
    }
}

// Paramerer visitor
public class ParameterVisitor : Visitor
{
    private readonly ParameterExpression node;

    public ParameterVisitor(ParameterExpression node) : base(node) => this.node = node;

    public override void Visit(string prefix)
    {
        Console.WriteLine($"{prefix}This is an {NodeType} expression type.");
        Console.WriteLine($"{prefix}Type: {node.Type}, Name: {node.Name}, ByRef: {node.IsByRef}");
    }
}

// Constant visitor
public class ConstantVisitor : Visitor
{
    private readonly ConstantExpression node;

    public ConstantVisitor(ConstantExpression node) : base(node) => this.node = node;

    public override void Visit(string prefix)
    {
        Console.WriteLine($"{prefix}This is an {NodeType} expression type.");
        Console.WriteLine($"{prefix}The type of the constant value is {node.Type}");
        Console.WriteLine($"{prefix}The value of the constant value is {node.Value}");
    }
}

// Conditional visitor
public class ConditionalVisitor : Visitor
{
    private readonly ConditionalExpression node;

    public ConditionalVisitor(ConditionalExpression node) : base(node) => this.node = node;

    public override void Visit(string prefix)
    {
        Console.WriteLine($"{prefix}This expression is a {NodeType} expression");

        var testVisitor = CreateFromExpression(node.Test);
        Console.WriteLine($"{prefix}The test for this expression is:");
        testVisitor.Visit(prefix + "\t");

        var trueVisitor = CreateFromExpression(node.IfTrue);
        Console.WriteLine($"{prefix}The true clause for this expression is:");
        testVisitor.Visit(prefix + "\t");

        var falseVisitor = CreateFromExpression(node.IfFalse);
        Console.WriteLine($"{prefix}The false clause for this expression is:");
        testVisitor.Visit(prefix + "\t");
    }
}

// Method call visitor
public class MethodCallVisitor : Visitor
{
    private readonly MethodCallExpression node;

    public MethodCallVisitor(MethodCallExpression node) : base(node) => this.node = node;

    public override void Visit(string prefix)
    {
        Console.WriteLine($"{prefix}This expression is a {NodeType} expression");

        if (node.Object is null)
        {
            Console.WriteLine($"{prefix}This is a static method call.");
        }
        else
        {
            Console.WriteLine($"{prefix}The receiver (this) is:");
            var receiverVisitor = CreateFromExpression(node.Object);
            receiverVisitor.Visit(prefix + "\t");
        }

        var methodInfo = node.Method;
        Console.WriteLine($"{prefix}The method name is {methodInfo.DeclaringType}.{methodInfo.Name}");

        // There is more here, like generic arguments, and so on.
        Console.WriteLine($"{prefix}The arguments are:");
        foreach (var arg in node.Arguments)
        {
            var argVisitor = CreateFromExpression(arg);
            argVisitor.Visit(prefix + "\t");
        }
    }
}
