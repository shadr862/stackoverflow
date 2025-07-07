using Acme.StackOverflow.Samples;
using Xunit;

namespace Acme.StackOverflow.EntityFrameworkCore.Applications;

[Collection(StackOverflowTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<StackOverflowEntityFrameworkCoreTestModule>
{

}
