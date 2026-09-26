using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.EventsManagement.ProjectDependencies.Rules;

internal sealed class InfrastructureLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_EVENTSMANAGEMENT_004",
        title: "Invalid Infrastructure layer reference",
        messageFormat: "{0} cannot reference {1}. Infrastructure layer may only reference Application.EventsManagement, shared Infrastructure, Messaging.EventsManagement, and Messages projects.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.InfrastructureEventsManagement;
    protected override bool AllowMessagesProjects => true;
    
    protected override IEnumerable<string> AllowedDirectReferences =>
    [
        KnownAssemblies.ApplicationEventsManagement,
        KnownAssemblies.DomainEventsManagement,
        KnownAssemblies.MessagingEventsManagement,
        KnownAssemblies.SharedInfrastructure,
        KnownAssemblies.SharedApplication,
        KnownAssemblies.SharedDomain
    ];
}