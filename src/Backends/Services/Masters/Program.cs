using Masters.Infrastructures;
using Masters.Models;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    //option.SwaggerDoc("orders", new OpenApiInfo { Title = "Order APIs", Version = "v1",Contact = new OpenApiContact(){  });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
});


builder.Services.AddDbContext<MasterDbContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(builder.Configuration.GetConnectionString("MastersContext"));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
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

DbInitializer.CreateDbIfNotExists(app);

app.Run();
