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
    var context = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (context.Database.EnsureCreated())
    {
        // Database was created, seed it with initial data
        SeedData.Initialize(context);
    }
}

app.Run();
