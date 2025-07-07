using Acme.StackOverflow.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.StackOverflow.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class StackOverflowController : AbpControllerBase
{
    protected StackOverflowController()
    {
        LocalizationResource = typeof(StackOverflowResource);
    }
}
