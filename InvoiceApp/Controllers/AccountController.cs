using InvoiceApp.Models.Models;
using InvoiceApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("/register")]
        public ActionResult Register([FromBody] RegiserUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("/login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string jwt = _accountService.LoginUser(dto);
            return Ok(jwt);
        }

        [HttpDelete("/{userId}")]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Delete([FromQuery] int userId)
        {
            _accountService.DeleteUser(userId);
            return Ok();
        }

    }
}
