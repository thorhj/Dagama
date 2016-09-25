using System;
using Dagama.Sitemap.Configuration;
using Sitecore.Configuration;

namespace Dagama.Sitemap
{
    public class PublishHandler
    {
        public virtual void Execute()
        {
            var configurationProvider = (IConfigurationProvider)Factory.CreateObject("/sitecore/dagama/sitemap/configurationProvider", true);
            if (configurationProvider == null)
            {
                throw new InvalidOperationException("Could not load configuration provider.");
            }
            //var generator = new Generator(configurationProvider, new Data);
            //generator.Generate();
        }
    }
}