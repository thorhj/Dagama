namespace Dagama.Sitemap.SearchEngines
{
    public interface ISearchEngine
    {
        string GetName();

        string GetPingAddress();
    }
}
