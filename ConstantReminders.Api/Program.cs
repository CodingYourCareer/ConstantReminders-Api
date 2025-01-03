using ConstantReminderApi.Handlers;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Data;
using ConstantReminders.Services;
using Serilog;
using System.Text.Json.Serialization;
using Asp.Versioning;
using ConstantReminderApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.AddNpgsqlDbContext<AppDbContext>(connectionName: "postgresdb", null,
    options => { options.UseSnakeCaseNamingConvention(); });

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-vqwp0iq3eaderlnm.us.auth0.com/"; //TODO: Can move this to configuration item or env var later
    options.Audience = "http://localhost:5000";  //TODO: Can move this to configuration item or env var later
});

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


var app = builder.Build();

app.MapDefaultEndpoints();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();

await using var log = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Console().CreateLogger();

log.Verbose("Preparing for development...");

await app.MigrateDatabaseAsync();

app.MapAEventEndpoints();

app.MapGet("/health", () => new {status = "healthy"});

await app.RunAsync();
public partial class Program { }
