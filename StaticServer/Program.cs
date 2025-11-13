using StaticServer;

// Keep Program.cs minimal: delegate all logic to StaticServerConfigurator
var builder = WebApplication.CreateBuilder(args);

// Optional: set a default wwwroot path for local development when not provided via
//  - CLI:   --wwwroot="/absolute/path/to/wwwroot"
//  - ENV:   STATICSERVER_WWWROOT=/absolute/path/to/wwwroot
// If you want to force a specific path by default, set the constant below.
// Note: This only applies when no explicit value is provided via CLI or ENV.
const string DevDefaultWwwroot = "";

bool wwwrootArgProvided = Environment.GetCommandLineArgs()
    .Any(a => a.StartsWith("--wwwroot=", StringComparison.OrdinalIgnoreCase));

if (!wwwrootArgProvided && string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("STATICSERVER_WWWROOT"))
    && !string.IsNullOrWhiteSpace(DevDefaultWwwroot))
{
    // Make it visible to the configurator which reads from the environment
    Environment.SetEnvironmentVariable("STATICSERVER_WWWROOT", DevDefaultWwwroot);
}

// Build options from CLI args and environment
var options = StaticServerConfigurator.BuildOptions(builder);

// Configure services (CORS, static web assets, etc.)
StaticServerConfigurator.ConfigureServices(builder, options);

var app = builder.Build();

// Configure middleware and endpoints
StaticServerConfigurator.ConfigureApp(app, options);

// Default port for convenience when not provided
var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
if (string.IsNullOrWhiteSpace(urls))
{
    app.Urls.Add("http://localhost:5005");
}

app.Run();
