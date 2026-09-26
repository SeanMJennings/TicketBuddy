namespace Architecture.EventsManagement.ProjectDependencies;

internal static class KnownAssemblies
{
    internal const string DomainEventsManagement = "Domain.EventsManagement";
    internal const string ApplicationEventsManagement = "Application.EventsManagement";
    internal const string ControllersEventsManagement = "Controllers.EventsManagement";
    internal const string InfrastructureEventsManagement = "Infrastructure.EventsManagement";
    internal const string MessagesEventsManagement = "Messages.EventsManagement";
    internal const string MessagingEventsManagement = "Messaging.EventsManagement";

    internal const string SharedDomain = "Domain";
    internal const string SharedApplication = "Application";
    internal const string SharedInfrastructure = "Infrastructure";

    internal static bool IsKnownAssembly(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;

        return name is DomainEventsManagement or ApplicationEventsManagement or ControllersEventsManagement
            or InfrastructureEventsManagement or MessagesEventsManagement or MessagingEventsManagement
            or SharedDomain or SharedApplication or SharedInfrastructure
            || name.StartsWith("Messages.");
    }
}