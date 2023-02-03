using System.Text.Json.Serialization;
using granito.bootstrapper.Configurations.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


public static partial class AddSwaggerCollection
    {
        public static IServiceCollection AddProtectedControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                
                config.Filters.Add(typeof(ValidateModelStateAttribute));
            });

            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    
                });

            
            services.AddSwaggerGen(c =>
            {
                
                // c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Granito",
                    Description = " Galdino Consumer teste"
                });
            });

            return services;
        }
    }