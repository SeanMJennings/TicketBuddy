using System.Text.RegularExpressions;

namespace Domain.Bookings.User;

public readonly struct Email : IEquatable<Email>
{
    private readonly string _value;

    public Email(string email)
    {
        Validation.BasedOn(errors =>
        {
            if (string.IsNullOrEmpty(email))
            {
                errors.Add($"{nameof(Email)} cannot be null or empty");
            }
            else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                errors.Add($"{nameof(Email)} must be valid");
            }
        });
        _value = email;
    }

    public override string ToString() => _value;
    public override bool Equals(object? obj) => obj is Email other && _value.Equals(other._value);
    public bool Equals(Email other) => _value.Equals(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public static bool operator ==(Email left, Email right) => left._value == right._value;
    public static bool operator !=(Email left, Email right) => left._value != right._value;
    public static implicit operator string(Email email) => email._value;
    public static implicit operator Email(string email) => new(email);
}