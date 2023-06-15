using orders;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateSlimBuilder(args);

var resource = ResourceBuilder.CreateDefault().AddService(serviceName: "Orders", serviceVersion: "1.0");

builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics=>
        {
            metrics
                .SetResourceBuilder(resource)
                .AddAspNetCoreInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting", 
                            "Microsoft.AspNetCore.Server.Kestrel")
                .AddPrometheusExporter();
        });

var app = builder.Build();

var sampleTodos = TodoGenerator.GenerateTodos().ToArray();
app.MapPrometheusScrapingEndpoint();
var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();

