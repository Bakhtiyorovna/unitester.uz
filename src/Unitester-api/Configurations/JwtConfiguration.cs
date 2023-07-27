using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace Unitester_api.Configurations
{
    public static class JwtConfiguration
    {
        public static void ConfigureJwtAuth(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration.GetSection("Jwt");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = config["Issuer"],
                        ValidateAudience=true,
                        ValidAudience = config["Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SecurityKey"]!))
                };
                });
        }
    }
}
