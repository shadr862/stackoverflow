using Microsoft.Extensions.Localization;
using Acme.StackOverflow.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Acme.StackOverflow;

[Dependency(ReplaceServices = true)]
public class StackOverflowBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<StackOverflowResource> _localizer;

    public StackOverflowBrandingProvider(IStringLocalizer<StackOverflowResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
