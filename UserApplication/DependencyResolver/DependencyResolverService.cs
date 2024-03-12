using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UserApplication.Interfaces;

namespace UserApplication.DependencyResolver;

public class DependencyResolverService
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserCrud, UserCrud>();
        serviceCollection.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }
}