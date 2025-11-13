namespace StaticServer;

/// <summary>
/// Options controlling how the Static Server exposes files and which features are enabled.
/// </summary>
public class StaticServerOptions
{
    /// <summary>
    /// Absolute path to the target wwwroot directory that will be served at '/'.
    /// </summary>
    public string WwwRootPath { get; set; } = string.Empty;

    /// <summary>
    /// Enables CORS middleware.
    /// </summary>
    public bool EnableCors { get; set; } = true;

    /// <summary>
    /// When CORS is enabled, allow any origin/header/method. Dev only.
    /// </summary>
    public bool AllowAllCors { get; set; } = true;

    /// <summary>
    /// Enables serving static web assets from referenced packages under '/_content/*'.
    /// </summary>
    public bool EnablePackageStaticAssets { get; set; } = true;

    /// <summary>
    /// Show a simple home page that lists discovered CSS files.
    /// </summary>
    public bool EnableHomePage { get; set; } = true;

    /// <summary>
    /// Adds a link to MudBlazor CSS under '/_content/MudBlazor/MudBlazor.min.css' on the home page.
    /// </summary>
    public bool IncludeMudBlazorLink { get; set; } = true;
}
