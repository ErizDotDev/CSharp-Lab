using CityDataMeasurements =
    System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<System.DateTime, DC.Lab.Measurement>>;
using DataMeasurements =
    System.Collections.Generic.Dictionary<System.DateTime, DC.Lab.Measurement>;

namespace DC.Lab;

public class HistoricalWeatherData
{
    readonly CityDataMeasurements storage = new CityDataMeasurements();

    public Measurement this[string city, DateTime date]
    {
        get
        {
            var cityData = default(DataMeasurements);

            if (!storage.TryGetValue(city, out cityData))
                throw new ArgumentOutOfRangeException(nameof(city), "City not found");

            var index = date.Date;
            var measure = default(Measurement);

            if (cityData.TryGetValue(index, out measure))
                return measure;

            throw new ArgumentOutOfRangeException(nameof(date), "Date not found");
        }
        set
        {
            var cityData = default(DataMeasurements);

            if (!storage.TryGetValue(city, out cityData))
            {
                cityData = new DataMeasurements();
                storage.Add(city, cityData);
            }

            // Strip out any time portion.
            var index = date.Date;
            cityData[index] = value;
        }
    }
}
