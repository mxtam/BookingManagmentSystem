using BookingManagmentSystem.BLL.DependencyInjection;
using BookingManagmentSystem.DAL.DependencyInjection;
using BookingManagmentSystem.API.Middlewares;
using Serilog;
using BookingManagmentSystem.DAL.Data;
using BookingManagmentSystem.DAL.Data.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddBusinessLayer();
builder.Services.AddDataLayer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{ 
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath, 
        includeControllerXmlComments: true);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    //Uncomment the following line if you want to apply migrations automatically on application startup
    //await context.Database.MigrateAsync();

    //Uncomment the following line if you want to clear the database before seeding
    //await SeedDatabase.ClearAsync(context);

    //Uncomment the following line if you want to seed the database with initial data
    //await SeedDatabase.SeedAsync(context);
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();