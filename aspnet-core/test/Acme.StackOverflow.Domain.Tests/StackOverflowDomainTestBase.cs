using Volo.Abp.Modularity;

namespace Acme.StackOverflow;

/* Inherit from this class for your domain layer tests. */
public abstract class StackOverflowDomainTestBase<TStartupModule> : StackOverflowTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
