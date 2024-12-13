using ConstantReminderApi.Handlers;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Data;
using ConstantReminders.Services;
using Serilog;
using System.Text.Json.Serialization;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


var app = builder.Build();

app.UseCors();

await using var log = new LoggerConfiguration().MinimumLevel.Verbose()
.WriteTo.Console().CreateLogger();

log.Verbose("Preparing for development...");
log.Information("Hi there! How are you ?");

app.MapAEventEndpoints();

app.MapGet("/health", () => new {status = "healthy"});

await app.RunAsync();