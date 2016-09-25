using JetBrains.Annotations;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace Dagama.Sitemap.Items
{
    public interface IDatabaseAdapter
    {
        Item GetItem(string path, [NotNull] Language language);
    }
}
