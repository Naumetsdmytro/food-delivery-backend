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

    // Register settings and services for Users
    services.AddSingleton<IUsersDatabaseSettings>(sp =>
        new UsersDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });
    services.AddSingleton<UsersService>();

    // Register settings and services for Products
    services.AddSingleton<IProductsDatabaseSettings>(sp =>
        new ProductsDatabaseSettings { ConnectionString = connectionString, DatabaseName = databaseName });
    services.AddSingleton<ProductsService>();

    // Register Swagger Generator
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
