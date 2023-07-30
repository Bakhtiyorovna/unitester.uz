using Microsoft.Extensions.Caching.Memory;
using Unitester_Service.Dtos.Auth;
using Unitester_Service.Interfaces.Auth;
using Unitester_Service.Interfaces.Comman;
using Unitester_Service.Interfaces.Contest;
using Unitester_Service.Interfaces.Notification;
using Unitester_Service.Interfaces.Tests;
using Unitester_Service.Interfaces.Users;
using Unitester_Service.Services.Auth;
using Unitester_Service.Services.Comman;
using Unitester_Service.Services.Contests;
using Unitester_Service.Services.Notification;
using Unitester_Service.Services.Tests;
using Unitester_Service.Services.Users;
////using Unitester_api.Configurations;

using Unitester_DataAccess.Interfaces.Contests;
using Unitester_DataAccess.Interfaces.Tests;
using Unitester_DataAccess.Interfaces.Users;
using Unitester_DataAccess.Repositories.Contests;
using Unitester_DataAccess.Repositories.Tests;
using Unitester_DataAccess.Repositories.Users;
using Unitester_DataAccess.Interfaces.Directions;
using Unitester_DataAccess.Repositories.Direction;
using Unitester_Service.Services.Directions;
using Unitester_Service.Interfaces.Directions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IContestRepository, ContestRepository>();
builder.Services.AddScoped<IDirectionRepository, DirectionRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IPaginator, Paginator>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IContestService, ContestService>();
builder.Services.AddSingleton<ISmsSender, SmsSender>();
builder.Services.AddScoped<IDirectionService, DirectionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


/*
using Microsoft.AspNetCore.Diagnostics;
using Unitester_api.Configurations;
using Unitester_api.Configurations.Layer;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.ConfigureJwtAuth();
builder.ConfigurSwaggerAuth();
builder.ConfigureCORSPolicy();
builder.ConfigureDataAccess();
builder.ConfigureServiceLayer();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

*/