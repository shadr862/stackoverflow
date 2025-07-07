using Acme.StackOverflow.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Acme.StackOverflow.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(StackOverflowEntityFrameworkCoreModule),
    typeof(StackOverflowApplicationContractsModule)
    )]
public class StackOverflowDbMigratorModule : AbpModule
{
}
