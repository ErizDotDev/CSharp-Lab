namespace DC.Lab;

public enum RootVegetables
{
    HorseRadish,
    Radish,
    Turnip
}

[Flags]
public enum Seasons
{
    None = 0,
    Summer = 1,
    Autumn = 2,
    Winter = 4,
    Spring = 8,
    All = Summer | Autumn | Winter | Spring
}

class Program
{
    public static void Main()
    {
        var availableIn = new Dictionary<RootVegetables, Seasons>();

        availableIn[RootVegetables.HorseRadish] = Seasons.All;
        availableIn[RootVegetables.Radish] = Seasons.Spring;
        availableIn[RootVegetables.Turnip] = Seasons.Spring | Seasons.Autumn;

        var seasons = new Seasons[] { Seasons.Summer, Seasons.Autumn, Seasons.Winter,
            Seasons.Spring };

        foreach (var season in seasons)
        {
            Console.WriteLine($"The following root vegetables are harvested in {season.ToString("G")}");

            foreach (KeyValuePair<RootVegetables, Seasons> item in availableIn)
            {
                if (((Seasons)item.Value & season) > 0)
                    Console.Write(string.Format("\t{0:G}\n", (RootVegetables)item.Key));
            }
        }
    }
}