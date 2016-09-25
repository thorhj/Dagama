namespace Dagama.Sitemap.Items
{
    public interface IDatabaseAdapterFactory
    {
        IDatabaseAdapter GetDatabase(string name);
    }
}
