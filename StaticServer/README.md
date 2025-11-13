# StaticServer (dev only)

Purpose: serve any project's wwwroot folder and easily expose CSS files via ngrok. The server is reusable: simply pass the path to the target wwwroot.

Prerequisites:
- .NET 8 SDK installed
- ngrok installed and authenticated (ngrok config add-authtoken ...)

Usage (macOS / Windows / Linux):
1. Open a terminal at the repository root
2. Start the static server pointing to a wwwroot:
   - Via CLI argument:  cd Source/StaticServer && dotnet run -- --wwwroot="/absolute/path/to/your/project/wwwroot"
   - Or via environment variable:  export STATICSERVER_WWWROOT="/absolute/path/to/your/project/wwwroot" && dotnet run
   - If nothing is provided, the server defaults to: ../EinhellConnect.Mobile/wwwroot
   The server listens on http://localhost:5005 by default (unless ASPNETCORE_URLS is set).
3. In another terminal, expose it via ngrok:
   ngrok http 5005
4. Copy the public URL provided by ngrok (e.g., https://XXXX.ngrok-free.app)
5. Open the home page: https://XXXX.ngrok-free.app/ — it automatically lists all CSS files found under /css of your wwwroot and (optionally) the MudBlazor link.

Features:
- Serves static files from the provided wwwroot path at root "/".
- Also serves package static web assets under /_content/* (e.g., MudBlazor) when available.
- Minimal home page that automatically lists CSS files under /css.
- Permissive CORS enabled by default (dev only), useful to reference these CSS files from other projects.

Configuration (env vars & arguments):
- --wwwroot=ABSOLUTE_PATH or STATICSERVER_WWWROOT=ABSOLUTE_PATH
- ASPNETCORE_URLS to change listening URL/port, e.g., ASPNETCORE_URLS=http://localhost:6000

Notes:
- CORS is open only to simplify sharing during development. Do not expose as-is in production.
- If you use MudBlazor, the public link is: /_content/MudBlazor/MudBlazor.min.css (when present).

Troubleshooting (NU1301 / 401 on a private NuGet source):
- This project is configured to use only nuget.org via StaticServer.csproj:
  - `<RestoreSources>https://api.nuget.org/v3/index.json</RestoreSources>`
  - `<RestoreIgnoreFailedSources>true</RestoreIgnoreFailedSources>`
- This isolates StaticServer from private feeds defined at solution/user level (nuget.config).
