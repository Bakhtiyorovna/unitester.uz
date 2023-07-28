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

namespace Unitester_api.Configurations.Layer
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
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
        }
    }
}
