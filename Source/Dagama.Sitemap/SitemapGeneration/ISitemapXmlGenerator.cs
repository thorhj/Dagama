using Dagama.Sitemap.Items;
using Dagama.Sitemap.Sites;
using JetBrains.Annotations;

namespace Dagama.Sitemap.SitemapGeneration
{
    public interface ISitemapXmlGenerator
    {
        [NotNull]
        string CreateSitemapXml([NotNull] ISitemapGenerationParameters sitemapGenerationParameters,
            [NotNull] ISiteContextFactory siteContextFactory, [NotNull] IDatabaseAdapterFactory databaseAdapterFactory, [NotNull] IItemFacade itemFacade);

        void SaveSitemap([NotNull] ISitemapGenerationParameters sitemapGenerationParameters, [NotNull] string sitemapXml);
    }
}
