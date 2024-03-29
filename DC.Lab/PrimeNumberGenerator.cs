namespace DC.Lab;

[Serializable]
internal class PrimeNumberGenerator
{
    private const int START = 2;

    private int maxUpperBound = 10_000_000;
    private int upperBound;
    private bool[] primeTable;
    private List<int> primes = new List<int>();

    public PrimeNumberGenerator(int upperBound)
    {
        if (upperBound > maxUpperBound)
        {
            string message = $"{upperBound} exceeds the maximum upper bound of" +
                $"{maxUpperBound}";

            throw new ArgumentOutOfRangeException(message);
        }

        this.upperBound = upperBound;

        //Create array and mark 0, 1 as not prime (True).
        primeTable = new bool[upperBound + 1];
        primeTable[0] = true;
        primeTable[1] = true;

        //Use Sieve of Eratosthenes to determine prime numbers.
        int upperBoundSqrt = (int)Math.Ceiling(Math.Sqrt(upperBound));
        for (int ctr = START; ctr <= upperBoundSqrt; ctr++)
        {
            if (primeTable[ctr])
                continue;

            for (int multiplier = ctr; multiplier <= upperBound / ctr; multiplier++)
            {
                if (ctr * multiplier <= upperBound)
                    primeTable[ctr * multiplier] = true;
            }
        }

        //Populate array with prime number information.
        int index = START;
        while (index != -1)
        {
            index = Array.FindIndex(primeTable, index, (flag) => !flag);
            if (index > -1)
            {
                primes.Add(index);
                index++;
            }
        }
    }

    public int[] GetAllPrimes() => primes.ToArray();

    public int[] GetPrimesFrom(int prime)
    {
        int start = primes.FindIndex((value) => value == prime);

        if (start >= 0)
            return primes.FindAll((value) => value >= prime).ToArray();

        throw new NotPrimeException(prime, $"{prime} is not a prime number.");
    }
}
