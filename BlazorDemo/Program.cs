using BlazorDemo.Components;
using BlazorDemo.Data;
using BlazorDemo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
var connectionString = builder.Configuration.GetConnectionString("PizzaStoreContext");
builder.Services.AddDbContext<PizzaStoreContext>(options =>
    options.UseSqlServer(connectionString));

// Register the API controller
builder.Services.AddControllers();

builder.Services.AddScoped<OrderState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map the API controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PizzaStoreContext>();

        // Ensure the database is created and apply any pending migrations
        context.Database.Migrate();

        // Database was created, seed it with initial data
        SeedData.Initialize(context);
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred creating the DB.");
        Console.WriteLine("An error occurred creating the DB.");
    }
    
}

app.Run();
