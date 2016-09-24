using System;
using System.Globalization;
using Dagama.Sitemap.Items;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;

namespace Dagama.Sitemap.Configuration
{
    public class ConfigurationItemFacade : IItemFacade
    {
        public virtual string IncludeItemField { get; set; }
        public virtual string ChangeFrequencyField { get; set; }
        public virtual string PriorityField { get; set; }

        public virtual bool IsIncludedInSitemap(Item item)
        {
            Assert.IsNotNull(item, "The item was null.");
            return item.Fields?[IncludeItemField]?.Value == "1";
        }

        public virtual ChangeFrequency? GetChangeFrequency(Item item)
        {
            Assert.IsNotNull(item, "The item was null.");
            ChangeFrequency changeFreq;
            if (Enum.TryParse(item.Fields?[ChangeFrequencyField]?.Value, out changeFreq))
            {
                return changeFreq;
            }

            return null;
        }

        public virtual decimal? GetPriority(Item item)
        {
            Assert.IsNotNull(item, "The item was null.");
            decimal priority;
            if (decimal.TryParse(item.Fields?[PriorityField]?.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out priority))
            {
                return priority;
            }

            return null;
        }

        public virtual string GetUrl(Item item)
        {
            Assert.IsNotNull(item, "The item was null.");
            return LinkManager.GetItemUrl(item);
        }

        public virtual DateTime? GetLastModified(Item item)
        {
            Assert.IsNotNull(item, "The item was null.");
            var lastmod = DateUtil.IsoDateToDateTime(item.Fields?[FieldIDs.Updated]?.Value);
            return lastmod;
        }
    }
}
