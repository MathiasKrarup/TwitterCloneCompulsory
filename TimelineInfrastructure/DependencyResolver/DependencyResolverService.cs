using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimelineInfrastructure.Interfaces;

namespace TimelineInfrastructure.DependencyResolver
{
    public class DependencyResolverService
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITimelineRepo, TimelineRepository>();
        }
    }
}
