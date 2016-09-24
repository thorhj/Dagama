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

        public IRobotsGenerationParameters RobotsGenerationParameters
        {
            get
            {
                if (_robotsGenerationParameters == null)
                {
                    lock (_robotsGenerationParametersInitLock)
                    {
                        if (_robotsGenerationParameters == null)
                        {
                            _robotsGenerationParameters = InitializeRobotsGenerationParametersFromConfig();
                        }
                    }
                }
                return _robotsGenerationParameters;
            }
        }
        private IRobotsGenerationParameters _robotsGenerationParameters;
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


        public ISearchEnginesNotificationParameters SearchEnginesNotificationParameters
        {
            get
            {
                if (_searchEnginesNotificationParameters == null)
                {
                    lock (_searchEngineNotificationParametersInitLock)
                    {
                        if (_searchEnginesNotificationParameters == null)
                        {
                            _searchEnginesNotificationParameters =
                                InitializeSearchEnginesNotificationParametersFromConfig();
                        }
                    }
                }
                return _searchEnginesNotificationParameters;
            }
        }

        private ISearchEnginesNotificationParameters _searchEnginesNotificationParameters;
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
        protected virtual IRobotsGenerationParameters InitializeRobotsGenerationParametersFromConfig()
        {
            var result = (IRobotsGenerationParameters)Factory.CreateObject("/sitecore/dagama/sitemap/robotsGenerationParameters", true);
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
        protected virtual ISearchEnginesNotificationParameters InitializeSearchEnginesNotificationParametersFromConfig()
        {
            var result = (ISearchEnginesNotificationParameters)Factory.CreateObject("/sitecore/dagama/sitemap/searchEnginesNotificationParameters", true);
            if (result == null) throw new InvalidOperationException("searchEnginesNotificationParameters");
            return result;
        }
    }
}
