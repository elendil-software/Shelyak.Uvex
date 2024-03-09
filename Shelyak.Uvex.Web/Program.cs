using System.Diagnostics;
using Shelyak.Uvex.Web.Components;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.Web;
using Shelyak.Uvex.Web.Configuration;
using Shelyak.Uvex.Web.Core;
using Shelyak.Uvex.Web.Core.Alpaca;
using Shelyak.Uvex.Web.Core.HttpClients;
using Shelyak.Uvex.Web.Core.Settings;
using Shelyak.Uvex.Web.Middleware;
using LoggerConfiguration = Serilog.LoggerConfiguration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.AppSettingsConfiguration()
    .CreateLogger();

try
{
    bool isStartedFromAscom = args.Contains("--ascom");
    
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.UseSerilog();
    
    builder.AddConfiguration();
    
// Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();
    
    builder.Services.AddBlazorBootstrap();

    builder.Services.AddControllers();

    builder.AddServices();
    builder.AddSwagger();


    WebApplication app = builder.Build();
    app.UseSerilogRequestLogging();

    app.UseOutputCache(); 

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // app.UseHsts();
    }
    else
    {
        app.UseMiddleware<RequestLoggingMiddleware>();
    }

    // Configure the HTTP request pipeline.
    app.UseSwagger(builder);
    
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();
    app.MapControllers();
    
    builder.StartBrowser(isStartedFromAscom);
    
    if (Process.GetProcesses().Count(p => p.ProcessName.Contains("Shelyak.Uvex.Web")) == 1)
    {
        app.Run();
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}