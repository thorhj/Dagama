using System;
using JetBrains.Annotations;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Sites;

namespace Dagama.Sitemap.Items
{
    public interface IItemFacade
    {
        /// <summary>
        /// Returns whether or not the given item should be included in the Sitemap XML.
        /// The logic to determine this is implemented in the concrete class and can follow any pattern.
        /// 
        /// The item passed to this method is expected to not be null.
        /// </summary>
        /// <param name="item">A non-null Sitecore item.</param>
        /// <returns>True if the item is to be included in the Sitemap XML, false otherwise.</returns>
        bool IsIncludedInSitemap([NotNull] Item item);

        /// <summary>
        /// Get the expected change frequency of the item. The change frequency is optional in the
        /// Sitemap XML, and will not be included if the result of this method is null.
        /// 
        /// The item passed to this method is expected to not be null.
        /// </summary>
        /// <param name="item">A non-null Sitecore item.</param>
        /// <returns>The change frequency of the item, or null if unspecified.</returns>
        ChangeFrequency? GetChangeFrequency([NotNull] Item item);

        /// <summary>
        /// Get the relative priority of the item. The priority is optional in the Sitemap XML, and
        /// will not be included if the result of this method is null. The priority should be
        /// between 0.0 and 1.0 (inclusive). If the value is outside of this range, the priorty
        /// will be ignored by the generator.
        /// 
        /// The item passed to this method is expected to not be null.
        /// </summary>
        /// <param name="item">A non-null Sitecore item.</param>
        /// <returns>The priority of the item, relative to other items. The priority should be a
        /// value between 0.0 and 1.0 (inclusive).</returns>
        decimal? GetPriority([NotNull] Item item);

        /// <summary>
        /// Get the URL of the item to be used in the Sitemap XML.
        /// 
        /// The item passed to this method is expected to not be null.
        /// </summary>
        /// <param name="item">A non-null Sitecore item.</param>
        /// <param name="site">The site for the item URL.</param>
        /// <param name="language">The language for the item URL.</param>
        /// <returns>The URL of the item.</returns>
        string GetUrl([NotNull] Item item, [NotNull] SiteContext site, [NotNull] Language language);

        /// <summary>
        /// Get the date and time of the last modification made to this item. If the value is null,
        /// it will be ignored by the generator.
        /// 
        /// The item passed to this method is expected to not be null.
        /// </summary>
        /// <param name="item">A non-null Sitecore item.</param>
        /// <returns>The date and time of the last modification to the item, or null if
        /// unspecified.</returns>
        DateTime? GetLastModified([NotNull] Item item);
    }
}
