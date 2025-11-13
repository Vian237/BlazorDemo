using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace StaticServer;

/// <summary>
/// Generates a minimal HTML index listing useful public links.
/// </summary>
internal static class HomePage
{
    public static string GenerateHtml(StaticServerOptions options, IWebHostEnvironment env)
    {
        var sb = new StringBuilder();
        sb.Append("<html><body style='font-family:sans-serif'>");
        sb.Append("<h3>StaticServer</h3>");

        // Display current target wwwroot
        sb.Append("<p><strong>Serving from</strong>: ");
        sb.Append(System.Net.WebUtility.HtmlEncode(options.WwwRootPath));
        sb.Append("</p>");

        // List CSS files from /css in the configured wwwroot
        var cssDir = string.IsNullOrWhiteSpace(options.WwwRootPath)
            ? null
            : Path.Combine(options.WwwRootPath, "css");

        var links = new List<string>();
        if (!string.IsNullOrWhiteSpace(cssDir) && Directory.Exists(cssDir))
        {
            foreach (var file in Directory.EnumerateFiles(cssDir, "*.css", SearchOption.TopDirectoryOnly))
            {
                var fileName = Path.GetFileName(file);
                // Public URL is relative to root, e.g., /css/app.min.css
                links.Add($"/css/{fileName}");
            }
        }

        // Optionally add MudBlazor asset link (served via package static web assets)
        if (options.IncludeMudBlazorLink && options.EnablePackageStaticAssets)
        {
            links.Add("/_content/MudBlazor/MudBlazor.min.css");
        }

        sb.Append("<ul>");
        foreach (var href in links.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
        {
            var safe = System.Net.WebUtility.HtmlEncode(href);
            sb.Append($"<li><a href='{safe}'>{safe}</a></li>");
        }
        sb.Append("</ul>");

        sb.Append("</body></html>");
        return sb.ToString();
    }
}
