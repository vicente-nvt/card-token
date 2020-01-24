using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CardToken.Application;
using CardToken.Application.CardCreation;
using CardToken.Application.CardValidation;
using CardToken.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace CardToken.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json")
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
             .AddEnvironmentVariables()
             .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Card Service",
                    Version = "v1",
                    Description = "A service to register user card",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            ConfigureDependencyInjection(services);
            ConfigureAuthorization(services);
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<CardCreation, CardCreator>();
            services.AddScoped<CardValidation, CardValidator>();
            services.AddSingleton<CardRepository, CardMemoryRepository>(); //I left this dependency injection as singleton because I'm using a dictionary as a database mock;
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            var secret = Configuration.GetSection("Secret").Value;
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                            SecurityAlgorithms.HmacSha256Signature).Key
                    };
                });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Produto Proxy Aggregator");
            });
        }
    }
}
