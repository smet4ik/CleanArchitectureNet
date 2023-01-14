using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application;
using DataAccess;
using DomainServices.Implementation;
using DomainServices.Interfaces;
using Email;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Integrations;
using Infrastructure.Interfaces.WebApp;
using Microsoft.EntityFrameworkCore;

namespace WebApp
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
            //Domain
            services.AddScoped<IOrderDomainService, OrderDomainService>();
            
            //Infrastructure
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddDbContext<IDbContext, AppDbContext>(builder =>
                builder.UseSqlite(Configuration.GetConnectionString("Sql")));
            
            //Application
            services.AddScoped<IOrderService, OrderService>();

            //Frameworks
            services.AddControllers();
            services.AddAutoMapper(typeof(MapperProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlerMiddleware();
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
