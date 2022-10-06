using Common.Logging;
using Contracts.Common.Interfaces;
using Customer.API;
using Customer.API.Controllers;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Start Customer API up");

try
{
    // Add services to the container.
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Services.AddControllers();
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
    
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

    builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));

    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
        .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryQueryBase<,,>))
        .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
        .AddScoped<ICustomerService, CustomerService>();

    var app = builder.Build();

    app.MapGet("/", () => "Welcome to Customer API!");
    
    app.MapCustomersAPI();

    app.MapControllers();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.SeedCustomerData().Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Customer API complete");
    Log.CloseAndFlush();
}