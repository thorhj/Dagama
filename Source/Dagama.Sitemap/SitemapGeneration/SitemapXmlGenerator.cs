using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Dagama.Sitemap.Items;
using Dagama.Sitemap.Sites;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Sites;
using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Dagama.Sitemap.SitemapGeneration
{
    public class SitemapXmlGenerator : ISitemapXmlGenerator
    {
        public virtual string CreateSitemapXml(ISitemapGenerationParameters sitemapGenerationParameters, ISiteContextFactory siteContextFactory,
            IDatabaseAdapterFactory databaseAdapterFactory, IItemFacade itemFacade)
        {

            const string sitemapXmlRootStartTag =
       @"<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"">";
            const string sitemapXmlRootEndTag = @"</urlset>";

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(sitemapXmlRootStartTag);
            foreach (var siteConfiguration in sitemapGenerationParameters.GetSites())
            {
                if (siteConfiguration == null) continue;
                foreach (var language in siteConfiguration.GetLanguages())
                {
                    if (language == null) continue;

                    var site = siteContextFactory.GetSite(siteConfiguration.GetName());
                    if (site == null)
                    {
                        throw new InvalidOperationException($"The site {siteConfiguration.GetName()} was not available.");
                    }

                    var database = databaseAdapterFactory.GetDatabase(siteConfiguration.GetDatabase());
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
                        .Where(itemFacade.IsIncludedInSitemap)
                        .ToList();

                    sitemapItems?.Add(rootItem);
                    foreach (var item in sitemapItems ?? Enumerable.Empty<Item>())
                    {
                        if (item == null) continue;
                        AppendItemXml(item, site, language, stringBuilder, itemFacade);
                    }
                }
            }
            stringBuilder.Append(sitemapXmlRootEndTag);

            var sitemapXml = stringBuilder.ToString();
            return sitemapXml;
        }

        public virtual void SaveSitemap(ISitemapGenerationParameters sitemapGenerationParameters, string sitemapXml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(sitemapXml);
            var filename = MainUtil.MapPath("/" + sitemapGenerationParameters.GetSitemapLocation());

            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                // ReSharper disable once ObjectCreationAsStatement
                new FileInfo(filename);
            }
            catch (Exception e) when (e is ArgumentException || e is PathTooLongException || e is NotSupportedException)
            {
                Log.Error("The filename was incorrect or unable to path could not be resolved.\n"
                    + $@"The filename given by configuration is ""{sitemapGenerationParameters.GetSitemapLocation() ?? "null"} and the resolved path is ""{filename ?? "null"}"".", this);
                throw;
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            xmlDocument.Save(filename);
        }

        private void AppendItemXml([NotNull] Item item,
            [NotNull] SiteContext site, [NotNull] Language language, [NotNull] StringBuilder stringBuilder, [NotNull] IItemFacade itemFacade)
        {
            var lastmod = itemFacade.GetLastModified(item)?.ToString("yyyy-MM-ddTHH:mm:sszzz");

            var changefreq = itemFacade.GetChangeFrequency(item);
            var priority = itemFacade.GetPriority(item);
            var url = itemFacade.GetUrl(item, site, language);
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

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
    }
}
