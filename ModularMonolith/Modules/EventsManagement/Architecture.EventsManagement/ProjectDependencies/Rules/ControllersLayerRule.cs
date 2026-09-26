using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.EventsManagement.ProjectDependencies.Rules;

internal sealed class ControllersLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_EVENTSMANAGEMENT_003",
        title: "Invalid Controllers layer reference",
        messageFormat: "{0} cannot reference {1}. Controllers layer may only reference Application.EventsManagement and its transitive dependencies.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.ControllersEventsManagement;
    protected override bool AllowMessagesProjects => true;
    
    protected override IEnumerable<string> AllowedDirectReferences =>
    [
        KnownAssemblies.ApplicationEventsManagement,
        KnownAssemblies.DomainEventsManagement,
        KnownAssemblies.SharedApplication,
        KnownAssemblies.SharedDomain
    ];
}