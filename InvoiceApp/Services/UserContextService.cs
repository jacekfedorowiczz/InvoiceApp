using InvoiceApp.Services.Contracts;
using System.Security.Claims;

namespace InvoiceApp.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetUserId => User is null ? null : (int?)int.Parse((User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
    }
}
