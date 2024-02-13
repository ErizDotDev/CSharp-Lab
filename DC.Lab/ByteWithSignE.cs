namespace DC.Lab;

public struct ByteWithSignE
{
    private SByte signValue;
    private Byte value;

    private const byte MaxValue = byte.MaxValue;
    private const int MinValue = -1 * byte.MaxValue;

    public static explicit operator ByteWithSignE(int value)
    {
        // Check for overflow
        if (value > ByteWithSignE.MaxValue || value < ByteWithSignE.MinValue)
            throw new OverflowException($"{value} is out of range of the ByteWithSignE" +
                $"data type");

        ByteWithSignE newValue;
        newValue.signValue = (SByte)Math.Sign(value);
        newValue.value = (byte)Math.Abs(value);
        return newValue;
    }

    public static explicit operator ByteWithSignE(uint value)
    {
        if (value > ByteWithSignE.MaxValue)
            throw new OverflowException($"{value} is out of range of the ByteWithSignE" +
                $"data type");

        ByteWithSignE newValue;
        newValue.signValue = 1;
        newValue.value = (byte)value;
        return newValue;
    }

    public override string ToString()
    {
        return (signValue * value).ToString();
    }
}
