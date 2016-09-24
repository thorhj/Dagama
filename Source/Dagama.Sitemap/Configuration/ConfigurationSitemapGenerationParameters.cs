using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Dagama.Sitemap.SitemapGeneration;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationSitemapGenerationParameters : ISitemapGenerationParameters
    {
        protected List<ISite> Sites;

        public IEnumerable<ISite> GetSites()
        {
            return Sites ?? Enumerable.Empty<ISite>();
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
