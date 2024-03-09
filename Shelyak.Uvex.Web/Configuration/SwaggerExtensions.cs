using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Shelyak.Uvex.Web.Configuration;

public static class SwaggerExtensions
{
    public static WebApplication UseSwagger(this WebApplication app, WebApplicationBuilder builder)
    {
        var enableSwagger = builder.Configuration.GetValue<bool>("OpenAPI:EnableSwagger");
        if (app.Environment.IsDevelopment() || enableSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "USIS Device API V1");
                c.RoutePrefix = "swagger";
            });
        }
        
        return app;
    }
    
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
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
        return builder;
    }
}