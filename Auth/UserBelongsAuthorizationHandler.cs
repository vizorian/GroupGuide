using Group_Guide.Auth.Model;
using Group_Guide.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Auth
{
    public class UserBelongsAuthorizationHandler : AuthorizationHandler<SameUserRequirement, IUserBelongableResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, IUserBelongableResource resource)
        {
            if(context.User.IsInRole(GroupGuideUserRoles.Admin) || resource.PlayerIds.Contains(context.User.FindFirst(CustomClaims.UserId).Value))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public record UserBelongsRequirement : IAuthorizationRequirement;
}
