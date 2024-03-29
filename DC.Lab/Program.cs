using DC.Lab;
using System.Reflection;

int limit = 10_000_000;
var primes = new PrimeNumberGenerator(limit);
int start = 1_000_001;

try
{
    var values = primes.GetPrimesFrom(start);
    Console.WriteLine($"There are {values.Length} prime numbers from {start} to " +
        $"{limit}");
}
catch (NotPrimeException e)
{
    Console.WriteLine($"{e.NonPrime} is not prime.");
    Console.WriteLine(e);
    Console.WriteLine("--------");
}

#pragma warning disable SYSLIB0024
var domain = AppDomain.CreateDomain("Domain2");

var gen = (PrimeNumberGenerator?)domain?.CreateInstanceAndUnwrap(
    typeof(Program).Assembly.FullName!, "PrimeNumberGenerator", true,
    BindingFlags.Default, null, new object[] { 1_000_000 }, null, null);

try
{
    start = 100;
    Console.WriteLine(gen?.GetPrimesFrom(start));
}
catch (NotPrimeException e)
{
    Console.WriteLine($"{e.NonPrime} is not prime.");
    Console.WriteLine(e);
    Console.WriteLine("-------");
}