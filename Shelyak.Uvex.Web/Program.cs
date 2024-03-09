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
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "USIS Device API",
            Version = "v1",
            Description = "API to perform operations on USIS compatible device",
            //TODO à compléter
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Shelyak Instruments",
                Url = new Uri("https://example.com/contact"),
            },
            License = new OpenApiLicense
            {
                Name = "License",
                Url = new Uri("https://example.com/license"),
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });


    var app = builder.Build();
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
    var enableSwagger = builder.Configuration.GetValue<bool>("OpenAPI:EnableSwagger");
    if (app.Environment.IsDevelopment() || enableSwagger)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


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