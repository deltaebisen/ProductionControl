using Microsoft.OpenApi.Models;
using Orders.Infrastructures;
using Orders.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    config
       .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
       .AddJsonFile("appsettings.json", true, true)
       .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
       .AddJsonFile("hostsettings.json", optional: true)
       .AddEnvironmentVariables()
       .Build();

});
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    //option.SwaggerDoc("orders", new OpenApiInfo { Title = "Order APIs", Version = "v1",Contact = new OpenApiContact(){  });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "Order APIs sandbox.");
        option.RoutePrefix = string.Empty;
    });
}


app.UseAuthorization();

app.MapControllers();

app.Run();
