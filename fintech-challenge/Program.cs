using System.Text;
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
    .AddJsonFile("../fintech-challenge.Tests/appsettings.test.json")
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

    var secretKey = configuration["Authentication:SecretKey"];

    var key = Encoding.ASCII.GetBytes(secretKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

    builder.Services.AddSingleton<IConfiguration>(configuration);
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<ILoginService, LoginService>();
    builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
    builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
    builder.Services.AddScoped<ICardsRepository, CardsRepository>();
    builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
    builder.Services.AddScoped<ICreatePeopleService, CreatePeopleService>();
    builder.Services.AddScoped<ICreateAccountService, CreateAccountService>();
    builder.Services.AddScoped<IGetPersonAccountsService, GetPersonAccountsService>();
    builder.Services.AddScoped<ICreateCardService, CreateCardService>();
    builder.Services.AddScoped<IGetAccountCardsService, GetAccountCardsService>();
    builder.Services.AddScoped<IGetPeopleCardsService, GetPeopleCardsService>();
    builder.Services.AddScoped<ICreateTransactionService, CreateTransactionService>();
    builder.Services.AddScoped<IGetAccountTransactionsService, GetAccountTransactionsService>();
    builder.Services.AddScoped<IGetAccountBalanceService, GetAccountBalanceService>();
    builder.Services.AddScoped<IRevertTransactionService, RevertTransactionService>();
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
public partial class Program { }

