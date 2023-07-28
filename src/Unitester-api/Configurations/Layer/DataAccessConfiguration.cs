
using Unitester_DataAccess.Interfaces.Contests;
using Unitester_DataAccess.Interfaces.Tests;
using Unitester_DataAccess.Interfaces.Users;
using Unitester_DataAccess.Repositories.Contests;
using Unitester_DataAccess.Repositories.Tests;
using Unitester_DataAccess.Repositories.Users;

namespace Unitester_api.Configurations.Layer
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            //-> DI containers, IoC containers
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITestRepository, TestRepository>();
            builder.Services.AddScoped<IContestRepository, ContestRepository>();
        }
    }
}
