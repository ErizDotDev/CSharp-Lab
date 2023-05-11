namespace DC.Lab;

public interface ICustomer
{
    IEnumerable<IOrder> PreviousOrders { get; }

    DateTime DateJoined { get; }
    DateTime? LastOrder { get; }
    string Name { get; }
    IDictionary<DateTime, string> Reminders { get; }

    public static void SetLoyaltyThresholds(
        TimeSpan ago,
        int minimumOrders = 10,
        decimal percentageDiscount = 0.10m)
    {
        length = ago;
        orderCount = minimumOrders;
        discountPercent = percentageDiscount;
    }

    private static TimeSpan length = new TimeSpan(265 * 2, 0, 0, 0); // TimeSpan for two years
    private static int orderCount = 10;
    private static decimal discountPercent = 0.10m;

    public decimal ComputeLoyaltyDiscount() => DefaultLoyaltyDiscount(this);

    protected static decimal DefaultLoyaltyDiscount(ICustomer c)
    {
        DateTime start = DateTime.Now - length;

        if ((c.DateJoined < start) && (c.PreviousOrders.Count() > orderCount))
        {
            return discountPercent;
        }

        return 0;
    }
}
