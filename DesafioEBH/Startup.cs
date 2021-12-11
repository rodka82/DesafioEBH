using API.Mapper;
using Application.Services;
using Application.Validators;
using Domain.Entities;
using Infra.Context;
using Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DesafioEBH
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
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite("Filename=edubrahub.db"));
            ConfigureDependencyInjection(services);

            var mapper = MapperGenerator.GenerateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioEBH", Version = "v1" });
            });
        }

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<DbContext, ApplicationDbContext>();

            services.AddScoped<IValidator<Store>, StoreValidator>();
            services.AddScoped<IStockItemValidator, StockItemValidator>();
            services.AddScoped<IValidator<Product>, ProductValidator>();

            services.AddScoped<IService<Store>, StoreService>();
            services.AddScoped<IStockItemService, StockItemService>();
            services.AddScoped<IService<Product>, ProductService>();

            services.AddScoped<IRepository<Store>, Repository<Store>>();
            services.AddScoped<IRepository<StockItem>, Repository<StockItem>>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var scope = app.ApplicationServices.CreateScope();
            //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //AddTestData(context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioEBH v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddTestData(ApplicationDbContext context)
        {
            //context.Add(model);
            //context.SaveChanges();
        }
    }
}
