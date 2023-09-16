﻿using System.Text;
using FintechChallenge.Database;
using FintechChallenge.Middlewares;
using FintechChallenge.Repositories;
using FintechChallenge.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

{
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "FintechChallenge.API",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "Gustavo",
                Email = "gustavofvv@gmail.com",

            }
        });

        var xmlFile = "fintech-challenge.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
    var databaseConnectionString = configuration["ConnectionStrings:DatabaseUrl"];
    builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseNpgsql(databaseConnectionString));

    builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
    builder.Services.AddScoped<ICreatePeopleService, CreatePeopleService>();
}


var app = builder.Build();
{
    app.AddErrorHandler();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});
    app.Run();
}

