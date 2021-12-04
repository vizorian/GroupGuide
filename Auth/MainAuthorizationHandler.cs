using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group_Guide.Auth
{
    public class MainAuthorizationHandler : IAuthorizationHandler
    {
        private readonly UserManager<GroupGuideUser> _userManager;

        public MainAuthorizationHandler(UserManager<GroupGuideUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();
            foreach (var requirement in pendingRequirements)
            {
                if(requirement is SameUserRequirement)
                {
                    if (context.Resource is IUserOwnedResource)
                    {
                        if (IsOwner(context.User, context.Resource as IUserOwnedResource))
                            context.Succeed(requirement);
                    }
                    else
                    {
                        if (IsOwnerBelongable(context.User, context.Resource as IUserBelongableResource))
                            context.Succeed(requirement);
                    }
                }
                else if(requirement is UserBelongsRequirement)
                {
                    if (await IsPartOfAsync(context.User, context.Resource as IUserBelongableResource))
                        context.Succeed(requirement);
                }

            }
        }

        private bool IsOwner(ClaimsPrincipal user, IUserOwnedResource resource)
        {
            if (user.IsInRole(GroupGuideUserRoles.Admin) || user.FindFirst(CustomClaims.UserId).Value == resource.UserId)
                return true;
            return false;
        }

        private bool IsOwnerBelongable(ClaimsPrincipal user, IUserBelongableResource resource)
        {
            if (user.IsInRole(GroupGuideUserRoles.Admin) || user.FindFirst(CustomClaims.UserId).Value == resource.Players.Last().Id)
                return true;
            return false;
        }

        private async Task<bool> IsPartOfAsync(ClaimsPrincipal user, IUserBelongableResource resource)
        {
            var playersNames = resource.Players.Select(p => p.Id).ToList();
            return user.IsInRole(GroupGuideUserRoles.Admin) ||
                   playersNames.Last() == user.FindFirst(CustomClaims.UserId).Value ||
                   playersNames.Contains(user.FindFirst(CustomClaims.UserId).Value);
        }
    }

    public record SameUserRequirement : IAuthorizationRequirement;
    public record UserBelongsRequirement : IAuthorizationRequirement;
}
