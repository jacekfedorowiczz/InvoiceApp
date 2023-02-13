using System.Security.Claims;

namespace InvoiceApp.Services.Contracts
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}
