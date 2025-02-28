namespace CustomInterfaces;

public abstract class ClonableType
{
    //Only derived types can support this "poymorphic interface".
    //Classes in other hierarchies have no access to this abstract member.
    public abstract object Clone();
}
