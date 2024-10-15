using System.Diagnostics;
using Shelyak.Uvex.Web.Components;
using Serilog;
using Shelyak.Uvex.Shared;
using Shelyak.Uvex.Web.Configuration;
using LoggerConfiguration = Serilog.LoggerConfiguration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.AppSettingsConfiguration()
    .CreateLogger();

try
{
    bool isStartedFromAscom = args.Contains(UvexConst.StartFromAscomArgument);
    
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.UseSerilog();
    
    builder.AddConfiguration();
    
    builder.Services.AddLocalization();
    
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();
    
    builder.Services.AddBlazorBootstrap();

    builder.AddServices();
    builder.AddFastEndpoints();


    WebApplication app = builder.Build();
    app.UseSerilogRequestLogging();

    app.UseLocalization();
    
    app.UseOutputCache(); 

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
    }
    
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();
    
    app.UseFastEndpoints(builder.Configuration);
    
    builder.StartBrowser(isStartedFromAscom);
    
    if (Process.GetProcesses().Count(p => p.ProcessName.Contains(UvexConst.UvexProcess)) == 1)
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