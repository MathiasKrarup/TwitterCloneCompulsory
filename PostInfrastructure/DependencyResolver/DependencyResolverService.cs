using Microsoft.Extensions.DependencyInjection;
using PostInfrastructure.Interfaces;

namespace PostInfrastructure.DependencyResolver;

public class DependencyResolverService
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPostRepository, PostRepository>();
    }
}