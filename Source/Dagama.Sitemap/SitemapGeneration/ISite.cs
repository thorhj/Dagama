using System.Collections.Generic;
using JetBrains.Annotations;
using Sitecore.Globalization;

namespace Dagama.Sitemap.SitemapGeneration
{
    public interface ISite
    {
        /// <summary>
        /// The name of the Sitecore site.
        /// </summary>
        /// <returns>The name of the Sitecore site.</returns>
        string GetName();

        /// <summary>
        /// The database to fetch the items from when building the sitemap for this site. This will
        /// usually be the web database containing the published content.
        /// </summary>
        /// <returns>The database to use when building the sitemap.</returns>
        string GetDatabase();

        /// <summary>
        /// A list of the languages that the Sitemap should contain for this site.
        /// </summary>
        /// <returns>A list of the languages that the Sitemap should contain for this site.</returns>
        [NotNull]
        IEnumerable<Language> GetLanguages();
    }
}
