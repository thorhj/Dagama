using System.Collections.Generic;
using System.Linq;
using Dagama.Sitemap.SitemapGeneration;
using Sitecore.Globalization;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationSite : ISite
    {
        public string Name { get; set; }

        public string Database { get; set; }

        protected List<Language> Languages { get; set; }

        public void AddLanguage(string language)
        {
            if (Languages == null)
            {
                Languages = new List<Language>();
            }
            var parsedLanguage = Language.Parse(language);
            Languages.Add(parsedLanguage);
        }

        public string GetName()
        {
            return Name;
        }

        public string GetDatabase()
        {
            return Database;
        }

        public IEnumerable<Language> GetLanguages()
        {
            return Languages ?? Enumerable.Empty<Language>();
        }
    }
}
