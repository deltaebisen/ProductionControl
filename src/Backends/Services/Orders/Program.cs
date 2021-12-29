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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
