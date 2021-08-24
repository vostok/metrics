using JetBrains.Annotations;

namespace Vostok.Metrics.Scraping
{
    [PublicAPI]
    public class ScrapeConfig
    {
        /// <summary>
        /// Whether or not to scrape on dispose.
        /// </summary>
        public bool? ScrapeOnDispose { get; set; }
    }
}