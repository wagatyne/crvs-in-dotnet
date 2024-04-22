using Crvs.Api.EndPoints;
using Crvs.Api.Middleware;
using Crvs.Infrastructure;
using Crvs.Core;
using Serilog;

//Initialize logger for logging stuff during start up
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();
try
{
    Log.Information("Application starting");
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    builder.Services.AddSerilog((service, loggerConfig) =>
    {
        loggerConfig.ReadFrom.Configuration(builder.Configuration);
        loggerConfig.ReadFrom.Services(service);
        loggerConfig.Enrich.FromLogContext();
        //loggerConfig.WriteTo.File(rollingInterval: RollingInterval.Hour)
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHttpContextAccessor();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddSwaggerGen();
    // Add services from the Core project to the container.
    builder.Services.AddCoreServices();

    // Add services from the Infrastructure project to the container.
    builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);
    Log.Information("Starting application build");
    var app = builder.Build();
    
    Log.Information("Application build successful");
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    app.UseSerilogRequestLogging(options => {
        options.EnrichDiagnosticContext = (diagContext, httpContext) => { 
            diagContext.Set("ClientIp",httpContext.Connection.RemoteIpAddress);
            diagContext.Set("ClientUserAgent", httpContext.Request.Headers.UserAgent);
        };
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    var summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

    app.RegisterBirthRegistrationEndPoints();
    Log.Information("Start running application");
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}