using InvoiceApp.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InvoiceApp.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Invoice>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Invoice invoice)
        {
            if (requirement.ResourceOperation == ResourceOperation.READ || requirement.ResourceOperation == ResourceOperation.CREATE)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value;

            if (invoice.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
