using System.Text.RegularExpressions;

namespace Domain.Bookings.User;

public readonly struct Name : IEquatable<Name>
{
    private readonly string _value;

    public Name(string name)
    {
        Validation.BasedOn(errors =>
        {
            if (string.IsNullOrEmpty(name))
            {
                errors.Add($"{nameof(Name)} cannot be null or empty");
            }
            else if (Regex.IsMatch(name, @"[^a-zA-Z\s]", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                errors.Add($"{nameof(Name)} can only have alphabetical characters");
            }
        });
        _value = name;
    }

    public override string ToString() => _value;
    public override bool Equals(object? obj) => obj is Name other && _value.Equals(other._value);
    public bool Equals(Name other) => _value.Equals(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public static bool operator ==(Name left, Name right) => left._value == right._value;
    public static bool operator !=(Name left, Name right) => left._value != right._value;
    public static implicit operator string(Name name) => name._value;
    public static implicit operator Name(string name) => new(name);
}