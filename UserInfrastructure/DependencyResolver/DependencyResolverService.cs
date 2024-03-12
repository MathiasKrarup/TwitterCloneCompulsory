using Microsoft.Extensions.DependencyInjection;
using UserInfrastructure.Interfaces;

namespace UserInfrastructure.DependencyResolverService;

public class DependencyResolverService
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserRepo, UserRepository>();
    }
}