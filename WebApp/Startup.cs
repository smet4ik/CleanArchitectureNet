using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using DataAccess;
using DataAccess.Interfaces;
using DomainServices.Implementation;
using DomainServices.Interfaces;
using Email.Implementation;
using Email.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Order.Commands.CreateOrder;
using UseCases.Utils;
using WebApp.Interfaces;
using WebApp.Services;

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
                builder.UseSqlServer(Configuration.GetConnectionString("MsSql")));
            //Application
            services.AddMediatR(typeof(CreateOrderCommand));
            services.AddScoped<ISecurityService, SecurityService>();
            
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
