using System.Collections.Generic;
using JetBrains.Annotations;

namespace Dagama.Sitemap.SearchEngines
{
    public interface ISearchEnginesNotifier
    {
        ///// <summary>
        ///// Whether or not the search engines are to be verified after sitemap generation.
        ///// Notification should only be enabled for production environments.
        ///// </summary>
        ///// <returns>True if the generator should notify search engines, false otherwise.</returns>
        //bool ShouldNotifySearchEngines();

        ///// <summary>
        ///// Get a list of search engines that should be notified.
        ///// </summary>
        ///// <returns>A list of search engines.</returns>
        //[NotNull]
        //IEnumerable<ISearchEngine> GetSearchEngines();

        void NotifySearchEngines();
    }
}
