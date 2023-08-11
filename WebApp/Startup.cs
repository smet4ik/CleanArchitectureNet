using System;
using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using AutoMapper;
using DataAccess.InMemory;
using DataAccess.Interfaces;
using Delivery.DHL;
using Delivery.Interfaces;
using DomainServices.Implementation;
using DomainServices.Interfaces;
using Email.Implementation;
using Email.Interfaces;
using Hangfire;
using Hangfire.Storage.SQLite;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseCases.BackgroundJobs;
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
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IBackgroundJobService, BackgroundJobService>();
            services.AddDbContext<IDbContext, AppDbContext>(builder =>
                builder.UseSqlite(Configuration.GetConnectionString("SqlLite")));
            //Application
            services.AddMediatR(typeof(CreateOrderCommand));
            services.AddScoped<ISecurityService, SecurityService>();
            
            //Frameworks
            services.AddControllers();
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddHangfire(h => h.UseSQLiteStorage());
            services.AddHangfireServer();
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

            app.UseHangfireServer();
            app.UseHangfireDashboard();
            RecurringJob
                .AddOrUpdate<UpdateDeliveryStatusJob>(
                    "UpdateDeliveryStatusJob",
                    job => job.ExecuteAsync(), 
                    Cron.Minutely);
        }
    }
}
