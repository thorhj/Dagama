using System;
using Dagama.Sitemap.Configuration;
using Dagama.Sitemap.Items;
using Dagama.Sitemap.SitemapGeneration;
using Dagama.Sitemap.Sites;
using Sitecore.Diagnostics;
using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Dagama.Sitemap
{
    public class Generator
    {
        [NotNull] private readonly IConfigurationProvider _configurationProvider;
        [NotNull] private readonly IDatabaseAdapterFactory _databaseAdapterFactory;
        [NotNull] private readonly ISiteContextFactory _siteContextFactory;
        [NotNull] private readonly ISitemapXmlGenerator _sitemapXmlGenerator;


        public Generator([NotNull] IConfigurationProvider configurationProvider,
            [NotNull] IDatabaseAdapterFactory databaseAdapterFactory, [NotNull] ISiteContextFactory siteContextFactory,
            [NotNull] ISitemapXmlGenerator sitemapXmlGenerator)
        {
            if (configurationProvider == null) throw new ArgumentNullException(nameof(configurationProvider));
            if (databaseAdapterFactory == null) throw new ArgumentNullException(nameof(databaseAdapterFactory));
            if (siteContextFactory == null) throw new ArgumentNullException(nameof(siteContextFactory));
            if (sitemapXmlGenerator == null) throw new ArgumentNullException(nameof(sitemapXmlGenerator));
            _configurationProvider = configurationProvider;
            _databaseAdapterFactory = databaseAdapterFactory;
            _siteContextFactory = siteContextFactory;
            _sitemapXmlGenerator = sitemapXmlGenerator;
        }

        public virtual void Generate()
        {
            var sitemap = _sitemapXmlGenerator.CreateSitemapXml(_configurationProvider.SitemapGenerationParameters, _siteContextFactory, _databaseAdapterFactory, _configurationProvider.ItemFacade);
            _sitemapXmlGenerator.SaveSitemap(_configurationProvider.SitemapGenerationParameters, sitemap);
            try
            {
                _configurationProvider.RobotsGenerator.SaveToRobotsFile();
            }
            catch (Exception e)
            {
                Log.Error("Saving to robots file failed. Process will continue.", e, this);
            }
            _configurationProvider.SearchEnginesNotifier.NotifySearchEngines();
        }
    }
}
