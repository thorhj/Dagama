using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Globalization;

namespace Dagama.Sitemap.SitemapGeneration
{
    public interface ISite
    {
        string GetName();

        string GetDatabase();

        IEnumerable<Language> GetLanguages();
    }
}
