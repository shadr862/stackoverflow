using Volo.Abp.Modularity;

namespace Acme.StackOverflow;

[DependsOn(
    typeof(StackOverflowDomainModule),
    typeof(StackOverflowTestBaseModule)
)]
public class StackOverflowDomainTestModule : AbpModule
{

}
