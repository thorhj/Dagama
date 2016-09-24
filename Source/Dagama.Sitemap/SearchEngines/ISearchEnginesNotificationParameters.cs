using System.Collections.Generic;

namespace Dagama.Sitemap.SearchEngines
{
    public interface ISearchEnginesNotificationParameters
    {
        bool ShouldNotifySearchEngines();
        IEnumerable<ISearchEngine> GetSearchEngines();
    }
}
