using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices()
                    .AddHandlers();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllUsersHandler>());

            return services;
        }



    }
}
