using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Dagama.Sitemap.SearchEngines;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationSearchEngineNotifier : ISearchEnginesNotifier
    {
        protected List<ISearchEngine> SearchEngines; 
        public bool ShouldNotifySearchEngines { get; set; }


        public void AddSearchEngine(XmlNode node)
        {
            throw new NotImplementedException();
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
