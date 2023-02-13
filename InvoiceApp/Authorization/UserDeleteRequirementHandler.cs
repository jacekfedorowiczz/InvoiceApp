using InvoiceApp.Entities;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceApp.Authorization
{
    public class UserDeleteRequirementHandler : AuthorizationHandler<UserDeleteRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserDeleteRequirement requirement, User user)
        {
            if (requirement.UserId == user.Id)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
