using Unitester_DataAccess.Interfaces.Tests;
using Unitester_DataAccess.Interfaces.Users;
using Unitester_DataAccess.Repositories.Tests;
using Unitester_DataAccess.Repositories.Users;
using Unitester_Service.Interfaces.Auth;
using Unitester_Service.Interfaces.Comman;
using Unitester_Service.Interfaces.Notification;
using Unitester_Service.Interfaces.Tests;
using Unitester_Service.Interfaces.Users;
using Unitester_Service.Services.Auth;
using Unitester_Service.Services.Comman;
using Unitester_Service.Services.Notification;
using Unitester_Service.Services.Tests;
using Unitester_Service.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IRepository, BaseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<ISmsSender, SmsSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
