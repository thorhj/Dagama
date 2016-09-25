using JetBrains.Annotations;

namespace Dagama.Sitemap.IO
{
    internal interface IFileHandler
    {
        void SaveSitemap([NotNull] string sitemap);
        void UpdateRobots();
    }
}
