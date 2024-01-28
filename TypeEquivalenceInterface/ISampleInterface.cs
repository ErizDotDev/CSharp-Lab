using System.Runtime.InteropServices;

namespace TypeEquivalenceInterface
{
    [ComImport]
    [Guid("5E3EFC5F-8347-43AA-984A-C89A5A0BA1AC")]
    public interface ISampleInterface
    {
        void GetUserInput();
        string UserInput { get; }
    }
}
