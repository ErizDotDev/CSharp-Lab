using System.Linq.Expressions;

namespace DC.Lab;

public class InterpretingExpressionTrees
{
    public static void Execute()
    {
        Expression<Func<int, bool>> exprTree = num => num < 5;

        ParameterExpression param = (ParameterExpression)exprTree.Parameters[0];
        BinaryExpression operation = (BinaryExpression)exprTree.Body;
        ParameterExpression left = (ParameterExpression)operation.Left;
        ConstantExpression right = (ConstantExpression)operation.Right;

        Console.WriteLine($"Decomposed expression: {param.Name} => {left.Name} {operation.NodeType} {right.Value}");
    }
}
