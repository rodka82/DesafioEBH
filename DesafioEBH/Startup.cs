using API.Mapper;
using API.Utils;
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
            SeedDatabase(app);

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

        private void SeedDatabase(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            AddData(context);
        }

        private void AddData(ApplicationDbContext context)
        {
            context.Reset();

            var product1 = new Product()
            {
                Id = 1,
                Name = "Produto 1",
                Price = 10.5
            };

            var product2 = new Product()
            {
                Id = 2,
                Name = "Produto 2",
                Price = 20.5
            };

            var product3 = new Product()
            {
                Id = 3,
                Name = "Produto 3",
                Price = 30.5
            };

            context.Products.AddRange(product1, product2, product3);

            var store1 = new Store()
            {
                Id = 1,
                Name = "Loja 1",
                Address = "Endereço Loja 1"
            };

            var store2 = new Store()
            {
                Id = 2,
                Name = "Loja 2",
                Address = "Endereço Loja 2"
            };

            var store3 = new Store()
            {
                Id = 3,
                Name = "Loja 3",
                Address = "Endereço Loja 3"
            };

            context.Stores.AddRange(store1, store2, store3);

            context.SaveChanges();
        }
    }
}
