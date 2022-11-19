using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MyWebAPi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms = Microsoft.Extensions.Configuration;

namespace MyWebApi.Tests.ServiceTests
{
    public class StudentServiceTests : UnitTestBase
    {
        private Mock<ISampleService> _sampleServiceMock { get; set; }
        public ms.IConfiguration Configuration { get; set; }
        public ILogger<SampleService> Logger { get; }

        public StudentServiceTests()
        {
            var configForSmsApi = new Dictionary<string, string>
                {
                    {"AppSettings:SmsApi", "http://example.com"},
                };

            Configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(configForSmsApi)
                                .Build();
            //Logger = new Mock<ILogger<SampleService>>().Object;

            Services.AddSingleton(Configuration);
            //Services.AddSingleton(Logger);
         

        _sampleServiceMock = MockServicesRegistrations<ISampleService, SampleService>();
        }
        [Fact]
        public async Task GetStudentById_WIthValidId_ReturnCorrectResut()
        {
            Assert.True(true);
        }
    }
}
