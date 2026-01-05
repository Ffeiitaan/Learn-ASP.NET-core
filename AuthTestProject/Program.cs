using AuthTestProject.Data;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using AuthTestProject.Services;
using AuthTestProject.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDatabase")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true

        };     
    }
);

builder.Services.AddScoped<IAuthorizationHandler, PermissionsRequirementsHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Permissions.Read, policy =>
    {
        policy.Requirements.Add(
            new PermissionsRequirements(Permissions.Read));
    });
    options.AddPolicy(Permissions.Delete, policy =>
    {
        policy.Requirements.Add(
            new PermissionsRequirements(Permissions.Delete));
    });
});

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IPermissionService, PermissionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();


