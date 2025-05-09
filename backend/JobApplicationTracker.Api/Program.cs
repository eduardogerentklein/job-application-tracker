using System.Reflection;
using System.Text.Json.Serialization;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Http.Json;
using Serilog;
using Web.Api;
using Web.Api.Extensions;
using Web.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder
    .AddLogging();

builder.Services
    .AddExceptionHandling()
    .AddOpenApiSupport()
    .AddSwagger();

builder.Services
    .AddRateLimit();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "JobApplicationTracker Api v1");
    });

    await app.ApplyMigrations();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging()
    .UseHttpsRedirection();

app.UseExceptionHandler();

app.MapEndpoints();

await app.RunAsync();

// Namespace Required for functional and integration tests to work.
namespace Web.Api
{
    public partial class Program;
}