using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Northwind_API.Data;
using Northwind_API.Services;
using Northwind_API.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// custom services
builder.Services.AddScoped<IOrderService, OrderService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure serilog.asp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("./Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
    

// register db context
builder.Services.AddDbContext<AppDBContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; // view Swagger UI at the app's root (https://localhost:<port>/)
    });

    // Able to view Swagger:
    // http://localhost:8008/swagger/v1/swagger.json
    // http://localhost:8008/index.html
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
