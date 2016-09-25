using System;
using Dagama.Sitemap.Configuration;
using Dagama.Sitemap.Items;
using Dagama.Sitemap.Robots;
using Dagama.Sitemap.SearchEngines;
using Dagama.Sitemap.SitemapGeneration;
using Dagama.Sitemap.Sites;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace Dagama.Sitemap.Tests
{
    [TestFixture]
    public class GeneratorTest
    {
        private IConfigurationProvider _configurationProvider;
        private IItemFacade _itemFacade;
        private IRobotsGenerator _robotsGenerator;
        private ISearchEnginesNotifier _searchEnginesNotifier;
        private ISitemapGenerationParameters _sitemapGenerationParameters;
        private IDatabaseAdapterFactory _databaseAdapterFactory;
        private ISiteContextFactory _siteContextFactory;
        private ISitemapXmlGenerator _sitemapXmlGenerator;

        [SetUp]
        public void SetUp()
        {
            _itemFacade = Substitute.For<IItemFacade>();
            _robotsGenerator = Substitute.For<IRobotsGenerator>();
            _searchEnginesNotifier = Substitute.For<ISearchEnginesNotifier>();
            _sitemapGenerationParameters = Substitute.For<ISitemapGenerationParameters>();
            _databaseAdapterFactory = Substitute.For<IDatabaseAdapterFactory>();
            _siteContextFactory = Substitute.For<ISiteContextFactory>();
            _sitemapXmlGenerator = Substitute.For<ISitemapXmlGenerator>();

            _configurationProvider = Substitute.For<IConfigurationProvider>();
            _configurationProvider.ItemFacade.Returns(_itemFacade);
            _configurationProvider.RobotsGenerator.Returns(_robotsGenerator);
            _configurationProvider.SearchEnginesNotifier.Returns(_searchEnginesNotifier);
            _configurationProvider.SitemapGenerationParameters.Returns(_sitemapGenerationParameters);
        }

        [Test]
        public void OrderOfOperations_AsExpected()
        {
            // Arrange
            var sitemap = @"<sitemap><url>hostname/home</url></sitemap>";
            _sitemapXmlGenerator
                .CreateSitemapXml(Arg.Any<ISitemapGenerationParameters>(), Arg.Any<ISiteContextFactory>(),
                    Arg.Any<IDatabaseAdapterFactory>(), Arg.Any<IItemFacade>())
                .Returns(sitemap);
            var generator = new Generator(_configurationProvider, _databaseAdapterFactory, _siteContextFactory,
                _sitemapXmlGenerator);

            // Act
            generator.Generate();

            // Assert
            _sitemapXmlGenerator.Received(1).CreateSitemapXml(_sitemapGenerationParameters, _siteContextFactory,
                _databaseAdapterFactory, _itemFacade);
            _sitemapXmlGenerator.Received(1).SaveSitemap(_sitemapGenerationParameters, sitemap);
            _robotsGenerator.Received(1).SaveToRobotsFile();
            _searchEnginesNotifier.Received(1).NotifySearchEngines();
        }

        [Test]
        public void CreatingSitemapThrowsException_ProcessStops()
        {
            // Arrange
            _sitemapXmlGenerator
                .CreateSitemapXml(Arg.Any<ISitemapGenerationParameters>(), Arg.Any<ISiteContextFactory>(),
                    Arg.Any<IDatabaseAdapterFactory>(), Arg.Any<IItemFacade>())
                .Throws(info => new InvalidOperationException());
            var generator = new Generator(_configurationProvider, _databaseAdapterFactory, _siteContextFactory,
                _sitemapXmlGenerator);

            // Act
            Assert.Throws<InvalidOperationException>(() => generator.Generate());

            // Assert
            _sitemapXmlGenerator.Received(1).CreateSitemapXml(_sitemapGenerationParameters, _siteContextFactory,
                _databaseAdapterFactory, _itemFacade);
            _sitemapXmlGenerator.Received(0).SaveSitemap(_sitemapGenerationParameters, Arg.Any<string>());
            _robotsGenerator.Received(0).SaveToRobotsFile();
            _searchEnginesNotifier.Received(0).NotifySearchEngines();
        }

        [Test]
        public void SavingSitemapThrowsException_ProcessStops()
        {
            // Arrange
            _sitemapXmlGenerator
                .When(s => s.SaveSitemap(Arg.Any<ISitemapGenerationParameters>(), Arg.Any<string>()))
                .Do(x => {throw new InvalidOperationException();});
            var generator = new Generator(_configurationProvider, _databaseAdapterFactory, _siteContextFactory,
                _sitemapXmlGenerator);

            // Act
            Assert.Throws<InvalidOperationException>(() => generator.Generate());

            // Assert
            _sitemapXmlGenerator.Received(1).CreateSitemapXml(_sitemapGenerationParameters, _siteContextFactory,
                _databaseAdapterFactory, _itemFacade);
            _sitemapXmlGenerator.Received(1).SaveSitemap(_sitemapGenerationParameters, Arg.Any<string>());
            _robotsGenerator.Received(0).SaveToRobotsFile();
            _searchEnginesNotifier.Received(0).NotifySearchEngines();
        }

        [Test]
        public void SavingRobotsThrowsException_ProcessContinues()
        {
            // Arrange
            _robotsGenerator
                .When(r => r.SaveToRobotsFile())
                .Do(x => { throw new InvalidOperationException(); });
            var generator = new Generator(_configurationProvider, _databaseAdapterFactory, _siteContextFactory,
                _sitemapXmlGenerator);

            // Act
            Assert.DoesNotThrow(() => generator.Generate());

            // Assert
            _sitemapXmlGenerator.Received(1).CreateSitemapXml(_sitemapGenerationParameters, _siteContextFactory,
                _databaseAdapterFactory, _itemFacade);
            _sitemapXmlGenerator.Received(1).SaveSitemap(_sitemapGenerationParameters, Arg.Any<string>());
            _robotsGenerator.Received(1).SaveToRobotsFile();
            _searchEnginesNotifier.Received(1).NotifySearchEngines();
        }

        [Test]
        public void SearchEngineNotificationThrowsException_ProcessStops()
        {
            // Arrange
            _searchEnginesNotifier
                .When(s => s.NotifySearchEngines())
                .Do(x => { throw new InvalidOperationException(); });
            var generator = new Generator(_configurationProvider, _databaseAdapterFactory, _siteContextFactory,
                _sitemapXmlGenerator);

            // Act
            Assert.Throws<InvalidOperationException>(() => generator.Generate());

            // Assert
            _sitemapXmlGenerator.Received(1).CreateSitemapXml(_sitemapGenerationParameters, _siteContextFactory,
                _databaseAdapterFactory, _itemFacade);
            _sitemapXmlGenerator.Received(1).SaveSitemap(_sitemapGenerationParameters, Arg.Any<string>());
            _robotsGenerator.Received(1).SaveToRobotsFile();
            _searchEnginesNotifier.Received(1).NotifySearchEngines();
        }
    }
}
