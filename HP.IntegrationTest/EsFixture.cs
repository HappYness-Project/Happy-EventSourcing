using Microsoft.Extensions.Configuration;

namespace HP.IntegrationTest
{
    public class EsFixture
    {
        public string ConnString { get; }
        public EsFixture()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            ConnString = config.GetConnectionString("mongo");
        }
    }
}
