using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.EventsManagement.Venue;

[JsonConverter(typeof(VenueNameConverter))]
public readonly struct VenueName : IEquatable<VenueName>
{
    private readonly string _value;
    
    public VenueName(string name)
    {
        Validation.BasedOn(errors =>
        {
            if (string.IsNullOrEmpty(name))
            {
                errors.Add($"{nameof(VenueName)} cannot be null or empty");
            }
        });
        _value = name;
    }

    public override string ToString() => _value;
    public override bool Equals(object? obj) => obj is VenueName other && _value.Equals(other._value);
    public bool Equals(VenueName other) => _value.Equals(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public static bool operator ==(VenueName left, VenueName right) => left._value == right._value;
    public static bool operator !=(VenueName left, VenueName right) => left._value != right._value;
    public static implicit operator string(VenueName venueName) => venueName._value;
    public static implicit operator VenueName(string name) => new(name);
}

public class VenueNameConverter : JsonConverter<VenueName>
{
    public override void Write(Utf8JsonWriter writer, VenueName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    public override VenueName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new VenueName(reader.GetString()!);
    }
}