using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.Bookings.ProjectDependencies.Rules;

internal sealed class MessagingLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_BOOKINGS_006",
        title: "Invalid Messaging layer reference",
        messageFormat: "{0} cannot reference {1}. Messaging layer may only reference Application.Bookings and Messages projects.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.MessagingBookings;
    protected override bool AllowMessagesProjects => true;

    protected override IEnumerable<string> AllowedDirectReferences =>
    [
        KnownAssemblies.ApplicationBookings,
        KnownAssemblies.DomainBookings,
        KnownAssemblies.SharedApplication,
        KnownAssemblies.SharedDomain
    ];
}
