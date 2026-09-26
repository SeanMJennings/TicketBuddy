using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.EventsManagement.ProjectDependencies.Rules;

internal sealed class DomainLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_EVENTSMANAGEMENT_001",
        title: "Invalid Domain layer reference",
        messageFormat: "{0} cannot reference {1}. Domain layer may only reference shared Domain.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.DomainEventsManagement;
    
    protected override IEnumerable<string> AllowedDirectReferences =>
    [
        KnownAssemblies.SharedDomain
    ];
}