using System.Diagnostics;
using flags_game.Models;
using flags_game.Pages.Shared.Components.FlagList;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<FlagAppDbContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString("Flags") ?? "Data Source=flags.db"
    )
);
    
var app = builder.Build();

// Caddy strips the public /flags prefix before proxying. Restore it as PathBase
// so Razor generates correct links while the test hostname can still use /.
app.Use((context, next) =>
{
    if (context.Request.Headers["X-Forwarded-Prefix"] == "/flags")
    {
        context.Request.PathBase = "/flags";
    }

    return next(context);
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Home}/{action=Index}");

app.Run();
