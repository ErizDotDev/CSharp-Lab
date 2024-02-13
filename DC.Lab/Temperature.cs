namespace DC.Lab;

public abstract class Temperature : IConvertible
{
    protected decimal temp;

    public Temperature(decimal temperature)
    {
        this.temp = temperature;
    }

    public decimal Value
    {
        get => this.temp;
        set => this.temp = value;
    }

    public override string ToString()
    {
        return temp.ToString(null as IFormatProvider) + "º";
    }

    public TypeCode GetTypeCode() => TypeCode.Object;

    public bool ToBoolean(IFormatProvider? provider)
    {
        throw new InvalidCastException($"Temperature-To-Boolean conversion is not " +
            $"supported.");
    }

    public byte ToByte(IFormatProvider? provider)
    {
        if (temp < Byte.MinValue || temp > Byte.MaxValue)
            throw new OverflowException($"{temp} is out of range of the Byte data type.");

        return (byte)temp;
    }

    public char ToChar(IFormatProvider? provider)
    {
        throw new InvalidCastException("Temperature-To-Char conversion is not supported.");
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        throw new InvalidCastException("Temperature-To-DateTime conversion is not supported.");
    }

    public decimal ToDecimal(IFormatProvider? provider) => temp;

    public double ToDouble(IFormatProvider? provider) => (double)temp;

    public short ToInt16(IFormatProvider? provider)
    {
        if (temp < Int16.MinValue || temp > Int16.MaxValue)
            throw new OverflowException($"{temp} is out of range of the Int16 data type.");

        return (short)Math.Round(temp);
    }

    public int ToInt32(IFormatProvider? provider)
    {
        if (temp < Int32.MinValue || temp > Int32.MaxValue)
            throw new OverflowException($"{temp} is out of range of the Int32 data type.");

        return (int)Math.Round(temp);
    }

    public long ToInt64(IFormatProvider? provider)
    {
        if (temp < Int64.MinValue || temp > Int64.MaxValue)
            throw new OverflowException($"{temp} is out of range of the Int64 data type.");

        return (long)Math.Round(temp);
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        if (temp < SByte.MinValue || temp > SByte.MaxValue)
            throw new OverflowException($"{temp} is out of range of the SByte data type.");

        return (sbyte)Math.Round(temp);
    }

    public float ToSingle(IFormatProvider? provider) => (float)temp;

    public virtual string ToString(IFormatProvider? provider) =>
        temp.ToString(provider) + "º";

    public virtual object ToType(Type conversionType, IFormatProvider? provider)
    {
        switch (Type.GetTypeCode(conversionType))
        {
            case TypeCode.Boolean:
                return this.ToBoolean(provider);
            case TypeCode.Byte:
                return this.ToByte(provider);
            case TypeCode.Char:
                return this.ToChar(provider);
            case TypeCode.DateTime:
                return this.ToDateTime(provider);
            case TypeCode.Decimal:
                return this.ToDecimal(provider);
            case TypeCode.Double:
                return this.ToDouble(provider);
            case TypeCode.Empty:
                throw new NullReferenceException("The target type is null.");
            case TypeCode.Int16:
                return this.ToInt16(provider);
            case TypeCode.Int32:
                return this.ToInt32(provider);
            case TypeCode.Int64:
                return this.ToInt64(provider);
            case TypeCode.Object:
                // Leave conversion of non-base types to derived classes.
                throw new InvalidCastException(String.Format("Cannot convert from " +
                    "Temperature to {0}.", conversionType.Name));
            case TypeCode.SByte:
                return this.ToSByte(provider);
            case TypeCode.Single:
                return this.ToSingle(provider);
            case TypeCode.String:
                IConvertible iconv = this;
                return iconv.ToString(provider);
            case TypeCode.UInt16:
                return this.ToUInt16(provider);
            case TypeCode.UInt32:
                return this.ToUInt32(provider);
            case TypeCode.UInt64:
                return this.ToUInt64(provider);
            default:
                throw new InvalidCastException("Conversion not supported.");
        }
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        if (temp < UInt16.MinValue || temp > UInt16.MaxValue)
            throw new OverflowException($"{temp} is out of range of the UInt16 data type.");

        return (ushort)Math.Round(temp);
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        if (temp < UInt32.MinValue || temp > UInt32.MaxValue)
            throw new OverflowException($"{temp} is out of range of the UInt32 data type.");

        return (uint)Math.Round(temp);
    }

    public ulong ToUInt64(IFormatProvider? provider)
    {
        if (temp < UInt64.MinValue || temp > UInt64.MaxValue)
            throw new OverflowException($"{temp} is out of range of the UInt64 data type.");

        return (ulong)Math.Round(temp);
    }
}
