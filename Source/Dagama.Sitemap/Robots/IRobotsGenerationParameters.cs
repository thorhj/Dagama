using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dagama.Sitemap.Robots
{
    /// <summary>
    /// I, Robot
    /// </summary>
    public interface IRobotsGenerationParameters
    {
        bool ShouldSaveToRobotsFile();
        string FileLocation();
    }
}
