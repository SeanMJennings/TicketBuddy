using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.EventsManagement.ProjectDependencies.Rules;

internal sealed class MessagesLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_EVENTSMANAGEMENT_005",
        title: "Invalid Messages layer reference",
        messageFormat: "{0} cannot reference {1}. Messages layer may only reference Domain.EventsManagement and shared Domain.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.MessagesEventsManagement;
    
    protected override IEnumerable<string> AllowedDirectReferences =>
    [
        KnownAssemblies.DomainEventsManagement,
        KnownAssemblies.SharedDomain
    ];
}