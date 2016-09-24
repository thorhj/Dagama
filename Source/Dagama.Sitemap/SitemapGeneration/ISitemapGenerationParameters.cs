using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dagama.Sitemap.SitemapGeneration
{
    public interface ISitemapGenerationParameters
    {
        IEnumerable<ISite> GetSites();
    }
}
