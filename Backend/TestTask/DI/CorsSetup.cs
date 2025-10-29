﻿
namespace TestTask.Api.DI
{
    public static class CorsSetup
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
        }
    }
}
