using System.Collections.Generic;
using JetBrains.Annotations;

namespace Dagama.Sitemap.SitemapGeneration
{
    public interface ISitemapGenerationParameters
    {
        /// <summary>
        /// Get the sites that should be have their items included in the Sitemap XML.
        /// </summary>
        /// <returns>A list of sites for which to build the Sitemap XML.</returns>
        [NotNull]
        IEnumerable<ISite> GetSites();
        
        string GetSitemapLocation();
    }
}
