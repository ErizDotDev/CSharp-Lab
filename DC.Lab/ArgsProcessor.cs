namespace DC.Lab;

public class ArgsProcessor
{
    private readonly ArgsAction action;

    public ArgsProcessor(ArgsAction action)
    {
        this.action = action;
    }

    public void Process(string[] args)
    {
        foreach (var arg in args)
        {
            action[arg]?.Invoke();
        }
    }
}

public class ArgsAction
{
    readonly private Dictionary<string, Action> argsActions = new Dictionary<string, Action>();

    public Action this[string s]
    {
        get
        {
            Action action;
            Action defaultAction = () => { Console.WriteLine("No valid option selected."); };
            return argsActions.TryGetValue(s, out action!) ? action : defaultAction;
        }
    }

    public void SetOption(string s, Action a)
    {
        argsActions[s] = a;
    }
}
