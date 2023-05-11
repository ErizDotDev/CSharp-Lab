namespace DC.Lab;

public interface IOrder
{
    DateTime Purchased { get; }
    decimal Cost { get; }
}
