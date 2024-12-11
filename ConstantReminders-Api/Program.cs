using Amazon.CloudWatchLogs;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



using var log = new LoggerConfiguration().MinimumLevel.Verbose()
.WriteTo.Console().CreateLogger();

log.Verbose("Preparing for development...");
log.Information("Hi there! How are you ?");


app.MapGet("/health", () => new {status = "healthy"});

app.Run();
public partial class Program { }
