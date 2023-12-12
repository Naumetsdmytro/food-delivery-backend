using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using mongodb_dotnet_example.Models;
using mongodb_dotnet_example.Services;

public class Startup
{

    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

         var mongoDBSettings = Configuration.GetSection("MongoDBSettings");
    Console.WriteLine($"ConnectionString: {mongoDBSettings["ConnectionString"]}");
    Console.WriteLine($"DatabaseName: {mongoDBSettings["DatabaseName"]}");
    }

    public IConfiguration Configuration { get; }

public void ConfigureServices(IServiceCollection services)
{
    // Add other service configurations as needed

    // Configure settings for UsersDatabase
    var mongoDBSettings = Configuration.GetSection("MongoDBSettings");
    var connectionString = mongoDBSettings["ConnectionString"];
    var databaseName = mongoDBSettings["DatabaseName"];

    services.AddSingleton<IUsersDatabaseSettings>(sp =>
        new UsersDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });

    // Register UsersService for dependency injection
    services.AddSingleton<UsersService>(sp =>
        new UsersService(connectionString, databaseName));

    services.AddSingleton<IProductDatabaseSettings>(sp =>
        new ProductDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });

    services.AddSingleton<ProductService>(sp =>
        new ProductService(connectionString, databaseName));


      services.AddSingleton<IOrdersDatabaseSettings>(sp =>
        new OrdersDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });

    services.AddSingleton<OrdersService>(sp =>
        new OrdersService(connectionString, databaseName));
    
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
    });

    // Add other service configurations as needed
    services.AddControllers();
}


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mongodb_dotnet_example v1"));
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
