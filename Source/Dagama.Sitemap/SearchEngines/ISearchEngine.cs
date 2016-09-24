namespace Dagama.Sitemap.SearchEngines
{
    public interface ISearchEngine
    {
        /// <summary>
        /// Get the name of the search engine, e.g. "Google"
        /// </summary>
        /// <returns>The name of the search engine.</returns>
        string GetName();

        /// <summary>
        /// The ping address of the search engine. The sitemap protocol says that the ping URLs
        /// should have a format like "http://searchengine.com/ping?sitemap=http%3A%2F%2Fwww.yoursite.com%2Fsitemap.xml".
        /// In this case, "http://searchengine.com/ping" would be the ping address.
        /// </summary>
        /// <returns>The ping address of the search engine.</returns>
        string GetPingAddress();
    }
}
