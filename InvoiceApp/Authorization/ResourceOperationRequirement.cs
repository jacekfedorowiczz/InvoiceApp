using Microsoft.AspNetCore.Authorization;

namespace InvoiceApp.Authorization
{
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; }

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
    }

    public enum ResourceOperation
    {
        CREATE,
        READ,
        UPDATE,
        DELETE
    }
}
