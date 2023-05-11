using DC.Lab;

var c = new SampleCustomer("customer one", new DateTime(2010, 5, 31))
{
    Reminders =
    {
        { new DateTime(2010, 8, 12), "child's birthday" },
        { new DateTime(2010, 11, 15), "anniversarry" }
    }
};

var o = new SampleOrder(new DateTime(2012, 6, 1), 5m);
c.AddOrder(o);

o = new SampleOrder(new DateTime(2013, 7, 4), 25m);
c.AddOrder(o);

ICustomer theCustomer = c;
ICustomer.SetLoyaltyThresholds(new TimeSpan(30, 0, 0, 0), 1, 0.25m);
Console.WriteLine($"Current discount: {theCustomer.ComputeLoyaltyDiscount()}");

Console.WriteLine($"Data about {c.Name}");
Console.WriteLine($"Joined on {c.DateJoined}. Made {c.PreviousOrders.Count()} orders, the last on {c.LastOrder}");
Console.WriteLine("Reminders");

foreach (var item in c.Reminders)
{
    Console.WriteLine($"\t{item.Value} on {item.Key}");
}

foreach (var order in c.PreviousOrders)
{
    Console.WriteLine($"Order on {order.Purchased} for {order.Cost}");
}
