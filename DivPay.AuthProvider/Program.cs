using DivPay.AuthProvider.Filters;
using DivPay.AuthProvider.Infrastructure;
using DivPay.DAL.Contracts;
using DivPay.DAL.Data;
using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting aplication...");

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<IJwtAuthenticationManager, JwtAuthenticationManager>();
    builder.Services.AddScoped<IUsuario, Usuario>();

    builder.Services.AddDbContext<DivPayContext>((provider, options) =>
    {
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

        options.UseSqlServer(builder.Configuration.GetConnectionString("DivPayContext"));
        options.UseLoggerFactory(loggerFactory);
        options.EnableSensitiveDataLogging();
    });

    // Add services to the container.
    builder.Services.AddControllers();

    builder.Services.AddMvc(opt =>
    {
        opt.Filters.Add(typeof(ErrorResponseExceptionFilter));
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseMiddleware<ExceptionMiddleware>();

    //app.UseHttpsRedirection();

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