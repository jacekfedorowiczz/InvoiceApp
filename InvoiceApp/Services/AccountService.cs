using InvoiceApp.Authentication;
using InvoiceApp.Authorization;
using InvoiceApp.Entities;
using InvoiceApp.Middlewares.Exceptions;
using InvoiceApp.Models.Models;
using InvoiceApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoiceApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly InvoiceAppDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public AccountService(InvoiceAppDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }


        public void RegisterUser(RegiserUserDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Coś poszło nie tak! Spróbuj proszę ponownie.");
            }

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
            };

            newUser.HashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public string LoginUser(LoginUserDto dto)
        {
            var user = _dbContext.Users.Include(x => x.Role).FirstOrDefault(x => x.Email == dto.Email);


            if (user == null)
            {
                throw new NotFoundException("Podano nieprawidłowy adres email lub hasło.");
            }

            var passwordCheck = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password);

            if (passwordCheck == PasswordVerificationResult.Failed)
            {
                throw new NotFoundException("Podano nieprawidłowy adres email lub hasło.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanWriteToken == true)
            {
                return tokenHandler.WriteToken(token);
            }
            else
            {
                throw new Exception("Coś poszło nie tak! Spróbuj proszę ponownie.");
            }

        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika po podanym id.");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, user, new UserDeleteRequirement(id)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Coś poszło nie tak. Spróbuj ponownie potem.");
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
