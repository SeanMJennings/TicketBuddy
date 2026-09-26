using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.Bookings.ProjectDependencies.Rules;

internal sealed class ControllersLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_BOOKINGS_003",
        title: "Invalid Controllers layer reference",
        messageFormat: "{0} cannot reference {1}. Controllers layer may only reference Application.Bookings and Domain.Bookings.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.ControllersBookings;
    protected override bool AllowMessagesProjects => true;

    protected override IEnumerable<string> AllowedDirectReferences =>
    [
        KnownAssemblies.ApplicationBookings,
        KnownAssemblies.DomainBookings,
        KnownAssemblies.SharedApplication,
        KnownAssemblies.SharedDomain
    ];
}
