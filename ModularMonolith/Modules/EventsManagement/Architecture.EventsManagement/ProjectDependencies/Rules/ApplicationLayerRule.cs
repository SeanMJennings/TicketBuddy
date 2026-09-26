using System.Collections.Generic;
using Architecture.ProjectDependencies;
using Microsoft.CodeAnalysis;

namespace Architecture.EventsManagement.ProjectDependencies.Rules;

internal sealed class ApplicationLayerRule : LayerRuleBase
{
    public override DiagnosticDescriptor Descriptor { get; } = new(
        id: "ARCH_EVENTSMANAGEMENT_002",
        title: "Invalid Application layer reference",
        messageFormat: "{0} cannot reference {1}. Application layer may only reference Domain.EventsManagement, shared Application/Domain, and Messages projects.",
        category: "Architecture",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    protected override string TargetAssembly => KnownAssemblies.ApplicationEventsManagement;
    protected override bool AllowMessagesProjects => true;
    
    protected override IEnumerable<string> AllowedDirectReferences =>
    [
        KnownAssemblies.DomainEventsManagement,
        KnownAssemblies.SharedApplication,
        KnownAssemblies.SharedDomain
    ];
}