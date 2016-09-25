using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Dagama.Sitemap.SitemapGeneration;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationSitemapGenerationParameters : ISitemapGenerationParameters
    {
        public string SitemapLocation { get; set; }

        protected List<ISite> Sites;

        public IEnumerable<ISite> GetSites()
        {
            return Sites ?? Enumerable.Empty<ISite>();
        }

        public string GetSitemapLocation()
        {
            return SitemapLocation;
        }

        public virtual void AddSite(XmlNode node)
        {
            if (Sites == null)
            {
                Sites = new List<ISite>();
            }
            
            throw  new NotImplementedException();
        }
    }
}
