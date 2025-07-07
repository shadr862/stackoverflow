using System.Threading.Tasks;

namespace Acme.StackOverflow.Data;

public interface IStackOverflowDbSchemaMigrator
{
    Task MigrateAsync();
}
