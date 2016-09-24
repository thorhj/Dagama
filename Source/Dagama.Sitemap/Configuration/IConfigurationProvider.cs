using Dagama.Sitemap.Items;
using Dagama.Sitemap.Robots;
using Dagama.Sitemap.SearchEngines;
using Dagama.Sitemap.SitemapGeneration;

namespace Dagama.Sitemap.Configuration
{
    public interface IConfigurationProvider
    {
        IItemFacade ItemFacade { get; }
        IRobotsGenerationParameters RobotsGenerationParameters { get; }
        ISitemapGenerationParameters SitemapGenerationParameters { get; }
        ISearchEnginesNotificationParameters SearchEnginesNotificationParameters { get; }
    }
}
