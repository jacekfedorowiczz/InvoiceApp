using InvoiceApp.Entities;
using InvoiceApp.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<InvoiceAppDbContext>(options =>
{
    options.UseSqlServer(cfg.GetConnectionString("InvoiceAppDbLocal"));
});


builder.Services.AddScoped<ErrorHandlingMiddleware>();















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
//app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
