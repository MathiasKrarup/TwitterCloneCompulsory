using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimelineApplication.Interfaces;

namespace TimelineApplication.DependencyResolver
{
    public class DependencyResolverService
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITimelineService, TimelineService>();
        }
    }
}
