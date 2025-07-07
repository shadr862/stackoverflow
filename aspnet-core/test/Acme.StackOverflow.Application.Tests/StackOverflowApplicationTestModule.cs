using Volo.Abp.Modularity;

namespace Acme.StackOverflow;

[DependsOn(
    typeof(StackOverflowApplicationModule),
    typeof(StackOverflowDomainTestModule)
)]
public class StackOverflowApplicationTestModule : AbpModule
{

}
