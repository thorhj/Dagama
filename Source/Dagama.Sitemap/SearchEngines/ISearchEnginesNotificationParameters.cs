using System.Collections.Generic;
using JetBrains.Annotations;

namespace Dagama.Sitemap.SearchEngines
{
    public interface ISearchEnginesNotificationParameters
    {
        bool ShouldNotifySearchEngines();

        [NotNull]
        IEnumerable<ISearchEngine> GetSearchEngines();
    }
}
