using Acme.StackOverflow.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.StackOverflow.Permissions;

public class StackOverflowPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(StackOverflowPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(StackOverflowPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StackOverflowResource>(name);
    }
}
