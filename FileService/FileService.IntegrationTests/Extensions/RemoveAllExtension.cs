using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.IntegrationTests.Extensions
{
    public static class RemoveAllExtension
    {
        public static IServiceCollection RemoveAll(this IServiceCollection services, bool predicate)
        {
            if (predicate)
                return services.RemoveAll(true);
                
            return services;
        }
    }
}
