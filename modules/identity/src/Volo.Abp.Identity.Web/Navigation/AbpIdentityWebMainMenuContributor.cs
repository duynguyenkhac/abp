﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.Identity.Localization;
using Volo.Abp.UI.Navigation;

namespace Volo.Abp.Identity.Web.Navigation
{
    public class AbpIdentityWebMainMenuContributor : IMenuContributor
    {
        public virtual async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name != StandardMenus.Main)
            {
                return;
            }

            var l = context.GetLocalizer<IdentityResource>();

            var identityMenuItem = new ApplicationMenuItem(IdentityMenuNames.GroupName, l["Menu:IdentityManagement"], icon: "fa fa-id-card-o");
            identityMenuItem.AddItem(new ApplicationMenuItem(IdentityMenuNames.Roles, l["Roles"], url: "~/Identity/Roles", requiredPermissionName: IdentityPermissions.Roles.Default));
            identityMenuItem.AddItem(new ApplicationMenuItem(IdentityMenuNames.Users, l["Users"], url: "~/Identity/Users", requiredPermissionName: IdentityPermissions.Users.Default));

            context.Menu.GetAdministration().AddItem(identityMenuItem);
        }
    }
}
