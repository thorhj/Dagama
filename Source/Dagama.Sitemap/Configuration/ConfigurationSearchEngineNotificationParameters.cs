using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Dagama.Sitemap.SearchEngines;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationSearchEngineNotificationParameters : ISearchEnginesNotificationParameters
    {
        protected List<ISearchEngine> SearchEngines; 

        public bool NotifySearchEngines { get; set; }

        public virtual bool ShouldNotifySearchEngines()
        {
            return NotifySearchEngines;
        }

        public virtual IEnumerable<ISearchEngine> GetSearchEngines()
        {

        }

        public void AddSearchEngine(XmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
