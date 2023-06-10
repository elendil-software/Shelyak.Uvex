using Microsoft.AspNetCore.Mvc;
using Shelyak.Usis;
using Shelyak.Uvex.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICommandSender, SerialPortCommandSender>();
builder.Services.AddSingleton<IServerTransactionIdProvider, ServerTransactionIdProvider>();

//Configuration
builder.Services.Configure<SerialPortSettings>(builder.Configuration.GetSection("SerialPortSettings"));

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
     options.ReportApiVersions = true;
     options.AssumeDefaultVersionWhenUnspecified = true;
     options.DefaultApiVersion = new ApiVersion(1, 0);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();