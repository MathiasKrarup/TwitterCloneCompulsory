using Microsoft.Extensions.DependencyInjection;
using PostApplication.Interfaces;

namespace PostApplication.DependencyResolver;

public class DependencyResolverService
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPostCrud, PostCrud>();
    }
}