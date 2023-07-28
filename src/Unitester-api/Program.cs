
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
