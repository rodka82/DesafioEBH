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
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = SIGNING_KEY,
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
            DataSeeder.Seed(context);
        }
    }
}
