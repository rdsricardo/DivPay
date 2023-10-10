using DivPay.API.Filters;
using DivPay.DAL.Contracts;
using DivPay.DAL.Data;
using DivPay.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting aplication...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddScoped<IUsuario, Usuario>();

    builder.Services.AddDbContext<DivPayContext>((provider, options) =>
    {
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

        options.UseSqlServer(builder.Configuration.GetConnectionString("DivPayContext"));
        options.UseLoggerFactory(loggerFactory);
        options.EnableSensitiveDataLogging();
    });

    var key = "D1vP4yP4g4m3nt051578!93@)";
    builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    // Add services to the container.
    builder.Services.AddControllers().
        AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddMvc(opt =>
    {
        opt.Filters.Add(typeof(ErrorResponseExceptionFilter));
    });

    //Desliga tratamento de erro de ModelState do framework (tratamento padrão quando o modelstate não é válido)
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //builder.Services.AddAuthorization();

    var app = builder.Build();

    //Serilog.Extensions.Hosting
    //app.UseSerilogRequestLogging(configure =>
    //{
    //    configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UsuarioId}/{NivelId}) responded {StatusCode} in {Elapsed:0.0000}ms";
    //});

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<UserMiddleware>();
    app.UseMiddleware<RequestResponseLoggingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    Log.Information("Server started. Hello, {Name}!", Environment.UserName);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlushAsync();
}