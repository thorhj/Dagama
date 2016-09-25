using Dagama.Sitemap.SearchEngines;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationSearchEngine : ISearchEngine
    {
        public string Name { get; set; }
        public string PingAddress { get; set; }

        public string GetName()
        {
            return Name;
        }

        public string GetPingAddress()
        {
            return PingAddress;
        }
    }
}
