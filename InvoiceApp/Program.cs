using FluentValidation;
using FluentValidation.AspNetCore;
using InvoiceApp.Authentication;
using InvoiceApp.Authorization;
using InvoiceApp.Data;
using InvoiceApp.Data.Contracts;
using InvoiceApp.Entities;
using InvoiceApp.Middlewares;
using InvoiceApp.Models.Models;
using InvoiceApp.Services;
using InvoiceApp.Services.Contracts;
using InvoiceApp.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;
var authenticationSettings = new AuthenticationSettings();

// Add services to the container.
cfg.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Bearer";
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<InvoiceAppDbContext>(options =>
{
    options.UseSqlServer(cfg.GetConnectionString("InvoiceAppDbLocal"));
});

builder.Services.AddFluentValidation();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, UserDeleteRequirementHandler>();

builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IInvoiceBuilder, InvoiceBuilder>();
builder.Services.AddScoped<IPDFGenerator, PDFGenerator>();

builder.Services.AddScoped<IValidator<Address>, AddressValidator>();
builder.Services.AddScoped<IValidator<CreateInvoiceDto>, CreateInvoiceDtoValidator>();
builder.Services.AddScoped<IValidator<InvoiceDto>, InvoiceDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();
builder.Services.AddScoped<IValidator<ModifyInvoiceDto>, ModifyInvoiceDtoValidator>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddScoped<IValidator<RegiserUserDto>, RegisterUserDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
//app.UseResponseCaching();
//app.UseStaticFiles();
app.UseRouting();
//app.UseCors("BlazorClient");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
