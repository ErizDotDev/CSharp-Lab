using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;

namespace Calculators;

public class TollCalculator
{
    public decimal CalculateToll(object vehicle) =>
        vehicle switch
        {
            Car { Passengers: 0 } => 2.00m + 0.50m,
            Car { Passengers: 1 } => 2.00m,
            Car { Passengers: 2 } => 2.00m - 0.50m,
            Car => 2.00m - 1.0m,

            Taxi { Fares: 0 } => 3.50m + 0.50m,
            Taxi { Fares: 1 } => 3.50m,
            Taxi { Fares: 2 } => 3.50m - 0.50m,
            Taxi => 3.50m - 1.0m,

            Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => 5.00m + 2.00m,
            Bus b when ((double)b.Riders / (double)b.Capacity) < 0.90 => 5.00m - 1.00m,
            Bus b => 5.00m,

            DeliveryTruck t when (t.GrossWeightClass > 5000) => 10.00m + 5.00m,
            DeliveryTruck t when (t.GrossWeightClass < 3000) => 10.00m - 2.00m,
            DeliveryTruck t => 10.00m,

            { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
            null => throw new ArgumentNullException(nameof(vehicle)),
        };
}
