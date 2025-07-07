using Volo.Abp.Settings;

namespace Acme.StackOverflow.Settings;

public class StackOverflowSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(StackOverflowSettings.MySetting1));
    }
}
