using System.Linq.Expressions;

namespace DC.Lab;

public class ExecutingExpressionTrees
{
    class SampleResource : IDisposable
    {
        private bool _isDisposed = false;

        public int Argument
        {
            get
            {
                if (!_isDisposed)
                    return 5;
                else
                    throw new ObjectDisposedException("Resource");
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
        }
    }

    public static void Execute()
    {
        ConvertExpressionToDelegate();
        CreateLambdaExpression();
        DemoWorkingDelegateWithCreateBoundFunc();
        DemoWorkingDelegateWithCreateBoundResource();
    }

    static void ConvertExpressionToDelegate()
    {
        Expression<Func<int>> add = () => 1 + 2;
        var func = add.Compile();
        var answer = func();
        Console.WriteLine(answer);
    }

    static void CreateLambdaExpression()
    {
        BinaryExpression be = Expression.Power(Expression.Constant(2d), Expression.Constant(3d));
        Expression<Func<double>> le = Expression.Lambda<Func<double>>(be);
        Func<double> compiledExpression = le.Compile();
        double result = compiledExpression();
        Console.WriteLine(result);
    }

    static void DemoWorkingDelegateWithCreateBoundFunc()
    {
        var functionToExecute = CreateBoundFunc();
        var result = functionToExecute(123);
        Console.WriteLine(result);
    }

    static Func<int, int> CreateBoundFunc()
    {
        var constant = 5;
        Expression<Func<int, int>> expression = (b) => constant + b;
        var rVal = expression.Compile();
        return rVal;
    }

    static void DemoWorkingDelegateWithCreateBoundResource()
    {
        var functionToExecute = CreateBoundResource();
        var result = functionToExecute(456);
        Console.WriteLine(result);
    }

    static Func<int, int> CreateBoundResource()
    {
        using (var constant = new SampleResource())
        {
            Expression<Func<int, int>> expression = (b) => constant.Argument + b;
            var rVal = expression.Compile();
            return rVal;
        }
    }
}
