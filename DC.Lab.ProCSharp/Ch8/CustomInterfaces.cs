namespace DC.Lab.ProCSharp.Ch8;

public abstract class CustomInterfaces
{
    //Only derived types can support this "poymorphic interface".
    //Classes in other hierarchies have no access to this abstract member.
    public abstract object Clone();
}
