namespace DC.Lab;

[Serializable]
internal class NotPrimeException : Exception
{
    private int notPrime;

    protected NotPrimeException() : base() { }

    public NotPrimeException(int value) : base($"{value} is not a prime number")
    {
        notPrime = value;
    }

    public NotPrimeException(int value, string message) : base(message)
    {
        notPrime = value;
    }

    public NotPrimeException(int value, string message, Exception innerException) :
        base(message, innerException)
    {
        notPrime = value;
    }

    public int NonPrime => notPrime;
}
