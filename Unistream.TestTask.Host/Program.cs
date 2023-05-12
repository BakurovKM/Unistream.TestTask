using System.Net;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Unistream.TestTask.Db;
using Unistream.TestTask.Host.Routes;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddDbContext<EntitiesContext>(opt => opt.UseInMemoryDatabase("Entities"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp
    .Run(async context => { context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; }));

app.MapEntitiesRoutes();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.Run();