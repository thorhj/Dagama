using System;
using System.IO;
using System.Text;
using Dagama.Sitemap.Robots;
using Sitecore;
using Sitecore.Diagnostics;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationRobotsGenerator : IRobotsGenerator
    {
        public virtual bool ShouldSaveToRobotsFile { get; set; }
        public virtual string FileLocation { get; set; }

        public virtual void SaveToRobotsFile()
        {
            if (!ShouldSaveToRobotsFile)
            {
                return;
            }

            try
            {
                var path = MainUtil.MapPath("/" + FileLocation);
                if (string.IsNullOrEmpty(path))
                {
                    throw new InvalidOperationException("Unable to map robots path to file locaiton.");
                }
                var stringBuilder = new StringBuilder(string.Empty);
                if (File.Exists(path))
                {
                    using (var streamReader = new StreamReader(path))
                    {
                        stringBuilder.Append(streamReader.ReadToEnd());
                        streamReader.Close();
                    }
                }
                var streamWriter = new StreamWriter(path, false);
                var sitemapText = "Sitemap: Sitemap.xml";
                if (!stringBuilder.ToString().Contains(sitemapText))
                {
                    stringBuilder.AppendLine(sitemapText);
                }
                streamWriter.Write(stringBuilder.ToString());
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Log.Error("An error occured when saving the robots.txt file.", e, this);
            }
        }
    }
}
