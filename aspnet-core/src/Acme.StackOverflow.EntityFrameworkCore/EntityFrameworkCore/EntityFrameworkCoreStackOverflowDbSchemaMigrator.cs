using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.StackOverflow.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.StackOverflow.EntityFrameworkCore;

public class EntityFrameworkCoreStackOverflowDbSchemaMigrator
    : IStackOverflowDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreStackOverflowDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the StackOverflowDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<StackOverflowDbContext>()
            .Database
            .MigrateAsync();
    }
}
