using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.StackOverflow.Data;

/* This is used if database provider does't define
 * IStackOverflowDbSchemaMigrator implementation.
 */
public class NullStackOverflowDbSchemaMigrator : IStackOverflowDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
