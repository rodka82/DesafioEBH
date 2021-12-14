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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace DesafioEBH
{
    public class Startup
    {
        private const string SECRET_KEY = "eyJhbGciOiJIUzI1NiJ9";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", jwtOptions =>
                {
                    jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        IssuerSigningKey =  SIGNING_KEY,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "https://localhost:44387/",
                        ValidAudience = "https://localhost:44387/",
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioEBH", Version = "v1" });
                c.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "JwtBearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "JwtBearer"
                              }
                          },
                         new string[] {}
                    }
                });
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
            services.AddScoped<IRepository<StockItem>, StockItemRepository>();
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

            app.UseAuthentication();

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

            var stockItem1 = new StockItem()
            {
                Id = 1,
                Product = product1,
                Store = store1,
                ProductId = product1.Id,
                StoreId = store1.Id,
                Quantity = 10
            };

            var stockItem2 = new StockItem()
            {
                Id = 2,
                Product = product2,
                Store = store2,
                ProductId = product2.Id,
                StoreId = store2.Id,
                Quantity = 20
            };

            var stockItem3 = new StockItem()
            {
                Id = 3,
                Product = product3,
                Store = store3,
                ProductId = product3.Id,
                StoreId = store3.Id,
                Quantity = 30
            };

            context.StockItems.AddRange(stockItem1, stockItem2, stockItem3);

            context.SaveChanges();
        }
    }
}
