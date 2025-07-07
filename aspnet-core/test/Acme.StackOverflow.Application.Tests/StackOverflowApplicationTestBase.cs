using Volo.Abp.Modularity;

namespace Acme.StackOverflow;

public abstract class StackOverflowApplicationTestBase<TStartupModule> : StackOverflowTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
