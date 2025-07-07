using Xunit;

namespace Acme.StackOverflow.EntityFrameworkCore;

[CollectionDefinition(StackOverflowTestConsts.CollectionDefinitionName)]
public class StackOverflowEntityFrameworkCoreCollection : ICollectionFixture<StackOverflowEntityFrameworkCoreFixture>
{

}
