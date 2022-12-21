using Microsoft.EntityFrameworkCore;
using api_poc.Data;
using api_poc.Models.IService;
using api_poc.Data.Services;
using Microsoft.OpenApi.Models;


namespace api_poc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DataContext")));

            services.AddScoped<DataInitializer>();

            services.AddScoped<IProductService, ProductService>();

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => $"{x.DeclaringType?.Name}.{x.Name}");
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "POC API", Version = "v1" });
            });

            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInitializer dataInitializer)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC API"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            dataInitializer.InitializeData().Wait();
        }
    }
}
