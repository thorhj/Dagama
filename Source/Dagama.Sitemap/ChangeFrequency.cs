namespace Dagama.Sitemap
{
    /// <summary>
    /// Enum for the possible values of change frequencies as defined in the protocol:
    /// http://www.sitemaps.org/protocol.html#changefreqdef
    /// </summary>
    public enum ChangeFrequency
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }
}
