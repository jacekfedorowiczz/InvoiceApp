using InvoiceApp.Models.Models;

namespace InvoiceApp.Services.Contracts
{
    public interface IAccountService
    {
        void RegisterUser(RegiserUserDto dto);
        string LoginUser(LoginUserDto dto);
        void DeleteUser(int id);
    }
}
