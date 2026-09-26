using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.Bookings.ProjectDependencies.Rules;

internal sealed class MessagesLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_BOOKINGS_005",
        title: "Invalid Messages layer reference",
        messageFormat: "{0} cannot reference {1}. Messages layer should have no project dependencies.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.MessagesBookings;

    protected override IEnumerable<string> AllowedDirectReferences => [];
}
