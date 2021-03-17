using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using URLShorteningService.DataAccess;
using URLShorteningService.DataAccess.Interfaces;
using URLShorteningService.Services;
using URLShorteningService.Services.Interfaces;
using URLShorteningService.Utils;

namespace URLShorteningService
{
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "URLShorteningService", Version = "v1" });
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IFileUpdaterService, FileUpdaterService>();
            services.AddTransient<IFileReaderService, FileReaderService>();
            services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
            services.AddTransient<IJsonWrapper, JsonWrapper>();

            switch (Configuration.GetSection("DataSource").Value)
            {
                case "Database":
                    services.AddTransient<IDataAccess, DatabaseDataAccess>();
                    break;
                case "FileSystem":
                    services.AddTransient<IDataAccess, FileSystemDataAccess>();
                    break;
                default:
                    services.AddTransient<IDataAccess, FileSystemDataAccess>();
                    break;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "URLShorteningService v1"));
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
