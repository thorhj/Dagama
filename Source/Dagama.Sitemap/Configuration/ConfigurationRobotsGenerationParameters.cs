using System;
using Dagama.Sitemap.Robots;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationRobotsGenerationParameters : IRobotsGenerationParameters
    {
        public virtual bool SaveToRobotsFile { get; set; }

        public virtual string FileLocation { get; set; }
        
        bool IRobotsGenerationParameters.ShouldSaveToRobotsFile()
        {
            return SaveToRobotsFile;
        }

        string IRobotsGenerationParameters.FileLocation()
        {
            return FileLocation;
        }
    }
}
