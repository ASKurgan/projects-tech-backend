using ASKTech.Framework.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Communication
{
    public static class FileServiceExtensions
    {
        public static IServiceCollection AddFileHttpCommunication(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileServiceOptions>(configuration.GetSection(FileServiceOptions.FILE_SERVICE));
            services.AddHttpClient<IFileService, FileHttpClient>((sp, config) =>
            {
                var fileOptions = sp.GetRequiredService<IOptions<FileServiceOptions>>().Value;

                config.BaseAddress = new Uri(fileOptions.Url);
            }).AddHttpMessageHandler<HttpTrackerHandler>();

            return services;
        }

        public static IServiceCollection AddFileServiceCacheDecorator(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Decorate<IFileService, FileServiceCachingDecorator>();

            return services;
        }
    }
}
