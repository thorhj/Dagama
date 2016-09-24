using Dagama.Sitemap.Items;
using Dagama.Sitemap.Robots;
using Dagama.Sitemap.SearchEngines;
using Dagama.Sitemap.SitemapGeneration;
using JetBrains.Annotations;

namespace Dagama.Sitemap.Configuration
{
    public interface IConfigurationProvider
    {
        [NotNull]
        IItemFacade ItemFacade { get; }

        [NotNull]
        IRobotsGenerationParameters RobotsGenerationParameters { get; }

        [NotNull]
        ISitemapGenerationParameters SitemapGenerationParameters { get; }

        [NotNull]
        ISearchEnginesNotificationParameters SearchEnginesNotificationParameters { get; }
    }
}
