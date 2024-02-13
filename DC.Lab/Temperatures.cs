namespace DC.Lab;

public class TemperatureCelsius : Temperature, IConvertible
{
    public TemperatureCelsius(decimal value) : base(value) { }

    public override string ToString() => this.ToString(null);

    public override string ToString(IFormatProvider? provider) =>
        temp.ToString(provider) + "°C";

    // If conversionType is implemented by another IConvertible method, call it.
    public override object ToType(Type conversionType, IFormatProvider? provider)
    {
        // For non-objects, call base method.
        if (Type.GetTypeCode(conversionType) != TypeCode.Object)
            return base.ToType(conversionType, provider);

        if (conversionType.Equals(typeof(TemperatureCelsius)))
            return this;
        else if (conversionType.Equals(typeof(TemperatureFahrenheit)))
            return new TemperatureFahrenheit((decimal)this.temp * 9 / 5 + 32);
        else
            throw new InvalidCastException($"Cannot convert from Temperature to " +
                $"{conversionType.Name}");
    }
}

public class TemperatureFahrenheit : Temperature, IConvertible
{
    public TemperatureFahrenheit(decimal value) : base(value) { }

    public override string ToString() => this.ToString(null);

    public override string ToString(IFormatProvider? provider) =>
        temp.ToString(provider) + "°F";

    public override object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (Type.GetTypeCode(conversionType) != TypeCode.Object)
            return base.ToType(conversionType, provider);

        if (conversionType.Equals(typeof(TemperatureFahrenheit)))
            return this;
        else if (conversionType.Equals(typeof(TemperatureCelsius)))
            return new TemperatureCelsius((decimal)(this.temp - 32) * 5 / 9);
        else
            throw new InvalidCastException($"Cannot convert from Temperature to " +
                $"{conversionType.Name}");
    }
}
