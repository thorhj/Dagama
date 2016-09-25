using Sitecore.Sites;

namespace Dagama.Sitemap.Sites
{
    public interface ISiteContextFactory
    {
        SiteContext GetSite(string name);
    }
}
