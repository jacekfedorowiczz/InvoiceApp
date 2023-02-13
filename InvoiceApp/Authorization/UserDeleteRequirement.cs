using Microsoft.AspNetCore.Authorization;

namespace InvoiceApp.Authorization
{
    public class UserDeleteRequirement : IAuthorizationRequirement
    {
        public int UserId { get; }

        public UserDeleteRequirement(int userId)
        {
            UserId = userId;
        }
    }
}
