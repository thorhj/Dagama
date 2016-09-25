using System;
using System.Collections.Generic;
using System.Xml;
using Dagama.Sitemap.SearchEngines;
using Sitecore.Configuration;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationSearchEngineNotifier : ISearchEnginesNotifier
    {
        protected List<ISearchEngine> SearchEngines; 
        public bool ShouldNotifySearchEngines { get; set; }


        public void AddSearchEngine(XmlNode node)
        {
            if (SearchEngines == null)
            {
                SearchEngines = new List<ISearchEngine>();
            }
            var searchEngine = Factory.CreateObject<ConfigurationSearchEngine>(node);
            SearchEngines.Add(searchEngine);
        }

        public virtual void NotifySearchEngines()
        {
            if (!ShouldNotifySearchEngines)
            {
                return;
            }

            throw new NotImplementedException();
        }
    }
}
