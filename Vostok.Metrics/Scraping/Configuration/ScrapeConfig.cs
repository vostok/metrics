using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Scraping.Configuration
{
    [PublicAPI]
    public class ScrapeConfig
    {
        public TimeSpan? ScrapePeriod { get; set; }
        public bool ScrapeOnDispose { get; set; }
    }
}