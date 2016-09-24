using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Sitecore.Globalization;

namespace Dagama.Sitemap.SitemapGeneration
{
    public interface ISite
    {
        string GetName();

        string GetDatabase();

        [NotNull]
        IEnumerable<Language> GetLanguages();
    }
}
