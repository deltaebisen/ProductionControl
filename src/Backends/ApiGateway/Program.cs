using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config
       .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
       .AddJsonFile("appsettings.json", true, true)
       .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
       .AddJsonFile("ocelot.json", true, true)
       .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
       .AddJsonFile("hostsettings.json", optional: true)
       .AddEnvironmentVariables()
       .Build();
    builder.Host.ConfigureServices(s =>
    {
        s.AddOcelot();
    });
});

builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    //todo: add your logging
});

var app = builder.Build();

app.UseRouting();

//app.UseHttpsRedirection();

//app.UseAuthorization();

await app.UseOcelot();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});
app.Run();
