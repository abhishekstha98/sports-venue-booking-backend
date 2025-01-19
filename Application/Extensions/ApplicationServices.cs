using Domain.Interface;
using Infrastructure.Repository;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;

namespace Application.Extensions
{
    public static class ApplicationServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericUnitOfWork, GenericUnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<IVenueService, VenueService>();
        }
    }
}
