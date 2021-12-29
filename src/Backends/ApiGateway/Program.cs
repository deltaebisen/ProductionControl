using Ocelot.DependencyInjection;
using Ocelot.Middleware;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
//{
//   config
//      .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
//      .AddJsonFile("appsettings.json", true, true)
//      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
//      .AddJsonFile("ocelot.json")
//      .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
//      .AddJsonFile("hostsettings.json", optional: true)
//      .AddEnvironmentVariables()
//      .Build();
//    builder.Host.ConfigureServices(s =>
//    {
//        s.AddOcelot();
//    });
//});

//builder.Host.ConfigureLogging((hostingContext, logging) =>
//{
//    //todo: add your logging
//});

//var app = builder.Build();


////app.UseHttpsRedirection();

////app.UseAuthorization();

//await app.UseOcelot();

//app.Run();

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                      .AddJsonFile("appsettings.json", true, true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                      .AddJsonFile("ocelot.json")
                      .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                      .AddJsonFile("hostsettings.json", optional: true);
            })
            //.UseSerilog(SeriLogger.Configure)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOcelot();
            //.AddCacheManager(settings => settings.WithDictionaryHandle());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });

        await app.UseOcelot();
    }
}