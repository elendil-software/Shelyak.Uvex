using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Shelyak.Usis;
using Shelyak.Usis.Responses;
using Shelyak.Uvex.WebApi;
using Shelyak.Uvex.WebApi.Settings;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.AppSettingsConfiguration()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

    var appsettingsUvexFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Shelyak/Uvex/appsettings-uvex.json");
    builder.Configuration.AddJsonFile(appsettingsUvexFilePath, optional: true, reloadOnChange: true);
    
// Add services to the container.
    builder.Services.AddSingleton<IUsisDevice, UsisDevice>();
    builder.Services.AddSingleton<ICommandSender, SerialPortCommandSender>();
    builder.Services.AddSingleton<IResponseParser, ResponseParser>();
    builder.Services.AddSingleton<IServerTransactionIdProvider, ServerTransactionIdProvider>();
    builder.Services.AddSingleton<ISerialPortSettingsWriter>(new SerialPortSettingsWriter(appsettingsUvexFilePath));

//Configuration
    builder.Services.Configure<SerialPortSettings>(builder.Configuration.GetSection("SerialPortSettings"));

    builder.Services.AddRazorPages();
    builder.Services.AddControllers();
    //    .AddJsonOptions(x =>
    //    {
    //        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //    });
    
    builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
    });

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
    if (app.Environment.IsDevelopment())
    {
        app.UseMiddleware<RequestLoggingMiddleware>();
        
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseSerilogRequestLogging();
    
    // Configure the HTTP request pipeline.
    
    var enableSwagger = builder.Configuration.GetValue<bool>("OpenAPI:EnableSwagger");
    if (app.Environment.IsDevelopment() || enableSwagger)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();
    app.MapControllers();
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}