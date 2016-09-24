using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml;
using Dagama.Sitemap.Configuration;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Dagama.Sitemap
{
    internal class Generator
    {
        private const string SitemapFileLocation = "Sitemap.xml";

        [JetBrains.Annotations.NotNull] private readonly IConfigurationProvider _configurationProvider;


        internal Generator([JetBrains.Annotations.NotNull] IConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null) throw new ArgumentNullException(nameof(configurationProvider));
            _configurationProvider = configurationProvider;
        }

        internal void Generate()
        {
            var sitemap = CreateSitemap();
            SaveSitemapToFile(sitemap);
            UpdateRobotsFile();
            NotifySearchEngines();
        }

        private string CreateSitemap()
        {
            const string sitemapXmlRootStartTag =
       @"<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"">";
            const string sitemapXmlRootEndTag = @"</urlset>";

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(sitemapXmlRootStartTag);
            string primaryUrl = null;
            foreach (var siteConfiguration in _configurationProvider.SitemapGenerationParameters.GetSites())
            {
                if (siteConfiguration == null) continue;
                foreach (var language in siteConfiguration.GetLanguages())
                {
                    var site = Factory.GetSite(siteConfiguration.GetName());
                    if (site == null)
                    {
                        throw new InvalidOperationException($"The site {siteConfiguration.GetName()} was not available.");
                    }

                    var database = Database.GetDatabase(siteConfiguration.GetDatabase());
                    if (database == null)
                    {
                        throw new InvalidOperationException($"The database {siteConfiguration.GetDatabase()} specified for site {siteConfiguration.GetName()} was not available.");
                    }

                    var rootItem = database.GetItem(site.StartPath, language);
                    if (rootItem == null)
                    {
                        Log.Warn($"Root item for site {siteConfiguration.GetName()} was not found. Proceeding with next site.", this);
                        continue;
                    }

                    var sitemapItems = rootItem.Axes?
                        .GetDescendants()?
                        .Where(_configurationProvider.ItemFacade.IsIncludedInSitemap)
                        .ToList();

                    sitemapItems?.Add(rootItem);
                    foreach (var item in sitemapItems ?? Enumerable.Empty<Item>())
                    {
                        if (item == null) continue;
                        AppendItemXml(item, stringBuilder);
                    }
                }
            }
            stringBuilder.Append(sitemapXmlRootEndTag);

            var sitemapXml = stringBuilder.ToString();
            return sitemapXml;
        }

        private void AppendItemXml([JetBrains.Annotations.NotNull] Item item, [JetBrains.Annotations.NotNull] StringBuilder stringBuilder)
        {
            var lastmod = _configurationProvider.ItemFacade.GetLastModified(item)?.ToString("yyyy-MM-ddTHH:mm:sszzz");

            var changefreq = _configurationProvider.ItemFacade.GetChangeFrequency(item);
            var priority = _configurationProvider.ItemFacade.GetPriority(item);
            var url = _configurationProvider.ItemFacade.GetUrl(item);

            stringBuilder.Append("<url>");
            stringBuilder.Append("<loc>");
            stringBuilder.Append(url);
            stringBuilder.Append("</loc>");
            if (lastmod != null)
            {
                stringBuilder.Append("<lastmod>");
                stringBuilder.Append(lastmod);
                stringBuilder.Append("</lastmod>");
            }

            if (changefreq != null)
            {
                stringBuilder.Append("<changefreq>");
                stringBuilder.Append(changefreq.ToString().ToLower());
                stringBuilder.Append("</changefreq>");
            }

            if (priority != null && priority >= 0.0m && priority <= 1.0m)
            {
                stringBuilder.Append("<priority>");
                stringBuilder.Append(priority.Value.ToString(CultureInfo.InvariantCulture));
                stringBuilder.Append("</priority>");
            }
            stringBuilder.Append("</url>");
        }

        private void SaveSitemapToFile([JetBrains.Annotations.NotNull] string sitemap)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(sitemap);
            var filename = MainUtil.MapPath("/" + SitemapFileLocation);
            if (string.IsNullOrEmpty(filename))
            {
                throw new InvalidOperationException("Unable to map Sitemap path to file location.");
            }
            xmlDocument.Save(filename);
        }

        private void UpdateRobotsFile()
        {
            if (!_configurationProvider.RobotsGenerationParameters.ShouldSaveToRobotsFile())
            {
                return;
            }

            try
            {
                var path = MainUtil.MapPath("/" + _configurationProvider.RobotsGenerationParameters.GetFileLocation());
                if (string.IsNullOrEmpty(path))
                {
                    throw new InvalidOperationException("Unable to map robots path to file locaiton.");
                }
                var stringBuilder = new StringBuilder(string.Empty);
                if (File.Exists(path))
                {
                    using (var streamReader = new StreamReader(path))
                    {
                        stringBuilder.Append(streamReader.ReadToEnd());
                        streamReader.Close();
                    }
                }
                var streamWriter = new StreamWriter(path, false);
                var sitemapText = "Sitemap: Sitemap.xml";
                if (!stringBuilder.ToString().Contains(sitemapText))
                {
                    stringBuilder.AppendLine(sitemapText);
                }
                streamWriter.Write(stringBuilder.ToString());
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Log.Error("An error occured when saving the robots.txt file.", e, this);
            }
        }

        private void NotifySearchEngines()
        {
            if (!_configurationProvider.SearchEnginesNotificationParameters.ShouldNotifySearchEngines())
            {
                return;
            }

            //foreach (var searchEngine in _configurationProvider.SearchEnginesNotificationParameters.GetSearchEngines())
            //{
            //    try
            //    {
            //        var httpClient = new HttpClient();
            //        var uri = $"{searchEngine.GetPingAddress()}?sitemap={baseUrl}/Sitemap.xml";
            //        var request = httpClient.GetAsync(uri);
            //        if (request.Result.StatusCode != HttpStatusCode.OK)
            //        {
            //            throw new Exception($"Submission to {uri} failed with status code: {request.Result.StatusCode} ({(int)request.Result.StatusCode})");
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Log.Error($"Error occurred when trying to submit Sitemap XML to search engine \"{searchEngine.Name}\" at URL {searchEngine.PingUrl}", e, this);
            //    }
            //}

        }
    }
}
