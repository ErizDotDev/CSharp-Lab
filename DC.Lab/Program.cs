using DC.Lab;

IEnumerable<string>? suits = Suits();
IEnumerable<string>? ranks = Ranks();

if ((suits is null) || (ranks is null))
    return;

var startingDeck = (from s in suits.LogQuery("Suit Generation")
                    from r in ranks.LogQuery("Rank Generation")
                    select new { Suit = s, Rank = r })
                   .LogQuery("Starting Deck")
                   .ToArray();

foreach (var card in startingDeck)
{
    Console.WriteLine(card);
}

var top = startingDeck.Take(26);
var bottom = startingDeck.Skip(26);
var shuffle = top.InterleaveSequenceWith(bottom);

var times = 0;
shuffle = startingDeck;

do
{
    //Console.WriteLine("Try shuffling!");

    shuffle = shuffle.Skip(26).LogQuery("Bottom Half")
        .InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top Half"))
        .LogQuery("Shuffle");

    foreach (var card in shuffle)
    {
        Console.WriteLine(card);
    }

    Console.WriteLine();
    times++;
}
while (!startingDeck.SequenceEquals(shuffle));

Console.WriteLine(times);

static IEnumerable<string> Suits()
{
    yield return "clubs";
    yield return "diamonds";
    yield return "hearts";
    yield return "spades";
}

static IEnumerable<string> Ranks()
{
    yield return "two";
    yield return "three";
    yield return "four";
    yield return "five";
    yield return "six";
    yield return "seven";
    yield return "eight";
    yield return "nine";
    yield return "ten";
    yield return "jack";
    yield return "king";
    yield return "queen";
    yield return "ace";
}