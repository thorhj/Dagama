using System;
using Dagama.Sitemap.Items;
using Dagama.Sitemap.Robots;
using Dagama.Sitemap.SearchEngines;
using Dagama.Sitemap.SitemapGeneration;
using JetBrains.Annotations;
using Sitecore.Configuration;

namespace Dagama.Sitemap.Configuration
{
    public class DefaultConfigurationProvider : IConfigurationProvider
    {
        public virtual IItemFacade ItemFacade
        {
            get
            {
                if (_itemFacade == null)
                {
                    lock (_itemFacadeInitLock)
                    {
                        if (_itemFacade == null)
                        {
                            _itemFacade = InitializeItemFacadeFromConfig();
                        }
                    }
                }

                return _itemFacade;
            }
        }
        private IItemFacade _itemFacade;
        [NotNull] private readonly object _itemFacadeInitLock = new object();

        public IRobotsGenerator RobotsGenerator
        {
            get
            {
                if (_robotsGenerator == null)
                {
                    lock (_robotsGenerationParametersInitLock)
                    {
                        if (_robotsGenerator == null)
                        {
                            _robotsGenerator = InitializeRobotsGenerationParametersFromConfig();
                        }
                    }
                }
                return _robotsGenerator;
            }
        }
        private IRobotsGenerator _robotsGenerator;
        [NotNull] private readonly object _robotsGenerationParametersInitLock = new object();

        public ISitemapGenerationParameters SitemapGenerationParameters
        {
            get
            {
                if (_sitemapGenerationParameters == null)
                {
                    lock (_sitemapGenerationParametersInitLock)
                    {
                        if (_sitemapGenerationParameters == null)
                        {
                            _sitemapGenerationParameters = InitializeSitemapGenerationParametersFromConfig();
                        }
                    }
                }
                return _sitemapGenerationParameters;
            }
        }
        private ISitemapGenerationParameters _sitemapGenerationParameters;
        [NotNull] private readonly object _sitemapGenerationParametersInitLock = new object();


        public ISearchEnginesNotifier SearchEnginesNotifier
        {
            get
            {
                if (_searchEnginesNotifier == null)
                {
                    lock (_searchEngineNotificationParametersInitLock)
                    {
                        if (_searchEnginesNotifier == null)
                        {
                            _searchEnginesNotifier =
                                InitializeSearchEnginesNotificationParametersFromConfig();
                        }
                    }
                }
                return _searchEnginesNotifier;
            }
        }

        private ISearchEnginesNotifier _searchEnginesNotifier;
        [NotNull]
        private readonly object _searchEngineNotificationParametersInitLock = new object();

        [NotNull]
        protected virtual IItemFacade InitializeItemFacadeFromConfig()
        {
            var result = (IItemFacade) Factory.CreateObject("/sitecore/dagama/sitemap/itemFacade", true);
            if (result == null) throw new InvalidOperationException("itemFacade");
            return result;
        }

        [NotNull]
        protected virtual IRobotsGenerator InitializeRobotsGenerationParametersFromConfig()
        {
            var result = (IRobotsGenerator)Factory.CreateObject("/sitecore/dagama/sitemap/robotsGenerator", true);
            if (result == null) throw new InvalidOperationException("robotsGenerationParameters");
            return result;
        }

        [NotNull]
        protected virtual ISitemapGenerationParameters InitializeSitemapGenerationParametersFromConfig()
        {
            var result = (ISitemapGenerationParameters)Factory.CreateObject("/sitecore/dagama/sitemap/sitemapGenerationParameters", true);
            if (result == null) throw new InvalidOperationException("sitemapGenerationParameters");
            return result;
        }

        [NotNull]
        protected virtual ISearchEnginesNotifier InitializeSearchEnginesNotificationParametersFromConfig()
        {
            var result = (ISearchEnginesNotifier)Factory.CreateObject("/sitecore/dagama/sitemap/searchEnginesNotifier", true);
            if (result == null) throw new InvalidOperationException("searchEnginesNotificationParameters");
            return result;
        }
    }
}
