using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.IntegrationTests
{
    public class FileServiceTestsBase : IClassFixture<IntegrationTestsWebFactory>
    {
        protected readonly HttpClient AppHttpClient;
        protected readonly HttpClient HttpClient;

        protected FileServiceTestsBase(
            IntegrationTestsWebFactory factory)
        {
            AppHttpClient = factory.CreateClient();
            HttpClient = new HttpClient();
        }
    }
}
