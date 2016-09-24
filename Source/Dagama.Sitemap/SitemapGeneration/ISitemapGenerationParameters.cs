using System.Collections.Generic;
using JetBrains.Annotations;

namespace Dagama.Sitemap.SitemapGeneration
{
    public interface ISitemapGenerationParameters
    {
        [NotNull]
        IEnumerable<ISite> GetSites();
    }
}
