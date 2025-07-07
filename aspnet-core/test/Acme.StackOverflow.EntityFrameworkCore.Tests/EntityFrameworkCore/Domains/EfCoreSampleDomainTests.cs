using Acme.StackOverflow.Samples;
using Xunit;

namespace Acme.StackOverflow.EntityFrameworkCore.Domains;

[Collection(StackOverflowTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<StackOverflowEntityFrameworkCoreTestModule>
{

}
