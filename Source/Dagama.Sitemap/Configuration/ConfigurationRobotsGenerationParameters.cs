using Dagama.Sitemap.Robots;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationRobotsGenerationParameters : IRobotsGenerationParameters
    {
        public virtual bool SaveToRobotsFile { get; set; }

        public virtual string FileLocation { get; set; }
        
        public bool ShouldSaveToRobotsFile()
        {
            return SaveToRobotsFile;
        }

        public string GetFileLocation()
        {
            return FileLocation;
        }
    }
}
