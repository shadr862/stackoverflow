using System;
using System.Collections.Generic;
using System.Text;
using Acme.StackOverflow.Localization;
using Volo.Abp.Application.Services;

namespace Acme.StackOverflow;

/* Inherit your application services from this class.
 */
public abstract class StackOverflowAppService : ApplicationService
{
    protected StackOverflowAppService()
    {
        LocalizationResource = typeof(StackOverflowResource);
    }
}
