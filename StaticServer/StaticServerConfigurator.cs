using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace StaticServer;

/// <summary>
/// Centralizes configuration for the Static Server so Program.cs can remain minimal.
/// </summary>
public static class StaticServerConfigurator
{
    private const string WwwrootEnvName = "STATICSERVER_WWWROOT";

    /// <summary>
    /// Build options from environment variables, command-line arguments, and reasonable defaults.
    /// </summary>
    public static StaticServerOptions BuildOptions(WebApplicationBuilder builder)
    {
        // Try to read explicit wwwroot path from CLI (--wwwroot) or environment variable
        var wwwrootFromArgs = builder.Configuration["wwwroot"] ?? GetArg(builder, "wwwroot");
        var wwwroot = string.IsNullOrWhiteSpace(wwwrootFromArgs)
            ? Environment.GetEnvironmentVariable(WwwrootEnvName)
            : wwwrootFromArgs;

        // Fallback to the original default path used in this repository
        if (string.IsNullOrWhiteSpace(wwwroot))
        {
            var contentRoot = builder.Environment.ContentRootPath; // .../Source/StaticServer
            wwwroot = Path.GetFullPath(Path.Combine(contentRoot, "..", "YourProject", "wwwroot"));
        }

        return new StaticServerOptions
        {
            WwwRootPath = wwwroot ?? string.Empty,
            EnableCors = true,
            AllowAllCors = true,
            EnablePackageStaticAssets = true,
            EnableHomePage = true,
            IncludeMudBlazorLink = true
        };
    }

    /// <summary>
    /// Configure DI and host settings according to options.
    /// </summary>
    public static void ConfigureServices(WebApplicationBuilder builder, StaticServerOptions options)
    {
        if (options.EnablePackageStaticAssets)
        {
            // Enable _content/* assets from referenced packages like MudBlazor
            builder.WebHost.UseStaticWebAssets();
        }

        if (options.EnableCors)
        {
            builder.Services.AddCors(cors =>
            {
                cors.AddDefaultPolicy(policy =>
                {
                    if (options.AllowAllCors)
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    }
                });
            });
        }
    }

    /// <summary>
    /// Configure middleware and endpoints.
    /// </summary>
    public static void ConfigureApp(WebApplication app, StaticServerOptions options)
    {
        if (options.EnableCors)
            app.UseCors();

        if (options.EnablePackageStaticAssets)
            app.UseStaticFiles(); // serves /_content/* from packages

        if (!string.IsNullOrWhiteSpace(options.WwwRootPath))
        {
            if (!Directory.Exists(options.WwwRootPath))
            {
                app.Logger.LogError("Target wwwroot directory does not exist: {Path}", options.WwwRootPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(options.WwwRootPath),
                RequestPath = string.Empty
            });
        }

        if (options.EnableHomePage)
        {
            app.MapGet("/", (IWebHostEnvironment env) =>
            {
                var html = HomePage.GenerateHtml(options, env);
                return Results.Content(html, "text/html");
            });
        }
    }

    private static string? GetArg(WebApplicationBuilder builder, string key)
    {
        // Minimal and robust way to parse --key=value args
        foreach (var a in Environment.GetCommandLineArgs())
        {
            if (a.StartsWith("--" + key + "=", StringComparison.OrdinalIgnoreCase))
            {
                return a[(key.Length + 3)..];
            }
        }
        return null;
    }
}
