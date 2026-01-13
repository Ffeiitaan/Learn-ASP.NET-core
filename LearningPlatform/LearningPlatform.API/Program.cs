using LearningPlatform.DataAccess.Postgers;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configurations = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<LearningDbContext>(
    options =>
    {
        options.UseNpgsql(configurations.GetConnectionString(nameof(LearningDbContext)));
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.Run();
