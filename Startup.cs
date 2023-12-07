public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Configure settings for UsersDatabase
        services.Configure<UsersDatabaseSettings>(
            Configuration.GetSection(nameof(UsersDatabaseSettings)));

        // Register UsersDatabaseSettings interface
        services.AddSingleton<IUsersDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<UsersDatabaseSettings>>().Value);

        // Register UsersService for dependency injection
        services.AddSingleton<UsersService>();

        // Register MVC Controllers
        services.AddControllers();

        // Register Swagger Generator
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
        });
    }
}
