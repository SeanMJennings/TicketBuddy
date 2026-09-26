namespace Architecture.Bookings.ProjectDependencies;

internal static class KnownAssemblies
{
    internal const string DomainBookings = "Domain.Bookings";
    internal const string ApplicationBookings = "Application.Bookings";
    internal const string ControllersBookings = "Controllers.Bookings";
    internal const string InfrastructureBookings = "Infrastructure.Bookings";
    internal const string MessagesBookings = "Messages.Bookings";
    internal const string MessagingBookings = "Messaging.Bookings";

    internal const string SharedDomain = "Domain";
    internal const string SharedApplication = "Application";
    internal const string SharedInfrastructure = "Infrastructure";

    internal static bool IsKnownAssembly(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;

        return name is DomainBookings or ApplicationBookings or ControllersBookings
            or InfrastructureBookings or MessagesBookings or MessagingBookings
            or SharedDomain or SharedApplication or SharedInfrastructure
            || name.StartsWith("Messages.");
    }
}
