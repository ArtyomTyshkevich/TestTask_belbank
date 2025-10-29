using FluentValidation.AspNetCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Mappers;
using TestTask.Infrastructure.DI;
using TestTask.Infrastructure.Features.Currency.Handlers.QuerieHandlers;
using TestTask.Infrastructure.Repositories;
using TestTask.Infrastructure.Repositories.UnitOfWork;
using TestTask.Api.DI;

namespace TestTask.Api.DI
{
    public static class BusinessLogicRegister
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();

            services.AddHttpClient<GetUsdRateQueryHandler>();
            services.ConfigureDatabase(configuration);
            services.ConfigureAuthentication(configuration);
            services.ConfigureCors(configuration);
            services.ConfigureSwagger();
            services.ConfigureIdentity();

            services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
            services.AddFluentValidationAutoValidation();
        }
    }
}
