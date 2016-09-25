using JetBrains.Annotations;

namespace Dagama.Sitemap.IO
{
    interface IDatabaseAdapterFactory
    {
        IDatabaseAdapter GetDatabase([NotNull] string name);
    }
}
