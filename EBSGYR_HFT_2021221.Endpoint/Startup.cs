using PKMXEN.Data;
using PKMXEN.Logic;
using PKMXEN.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PKMXEN.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ICarrierLogic, CarrierLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IParcelLogic, ParcelLogic>();
            services.AddTransient<ICarrierRepository, CarrierRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IParcelRepository, ParcelRepository>();
            services.AddSingleton<ShippingDbContext, ShippingDbContext>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:45495"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
