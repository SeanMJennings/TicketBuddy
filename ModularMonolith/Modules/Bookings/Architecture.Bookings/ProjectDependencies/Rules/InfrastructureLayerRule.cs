using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.Bookings.ProjectDependencies.Rules;

internal sealed class InfrastructureLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_BOOKINGS_004",
        title: "Invalid Infrastructure layer reference",
        messageFormat: "{0} cannot reference {1}. Infrastructure layer may only reference Application.Bookings, shared Infrastructure/Domain, Messaging.Bookings, and Messages projects.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.InfrastructureBookings;
    protected override bool AllowMessagesProjects => true;

    protected override IEnumerable<string> AllowedDirectReferences =>
    [
        KnownAssemblies.ApplicationBookings,
        KnownAssemblies.DomainBookings,
        KnownAssemblies.MessagingBookings,
        KnownAssemblies.SharedInfrastructure,
        KnownAssemblies.SharedApplication,
        KnownAssemblies.SharedDomain
    ];
}
