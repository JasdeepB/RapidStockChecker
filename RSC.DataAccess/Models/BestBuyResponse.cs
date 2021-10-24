using System.Collections.Generic;

namespace RSC.Models
{
    class BestBuyResponse
    {
        public int from { get; set; }
        public int to { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public string queryTime { get; set; }
        public string totalTime { get; set; }
        public bool partial { get; set; }
        public string canonicalUrl { get; set; }
        public List<Products> products { get; set; }
    }

    class Products
    {
        public string name { get; set; }
        public int sku { get; set; }
        public string url { get; set; }
        public bool onlineAvailability { get; set; }
        public bool inStoreAvailability { get; set; }
        public string inStoreAvailabilityUpdateDate { get; set; }
        public string orderable { get; set; }
        public string onlineAvailabilityUpdateDate { get; set; }
        public List<Images> images { get; set; }
    }

    class Images
    {
        public string rel { get; set; }
        public string unitOfMeasure { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string href { get; set; }
        public bool primary { get; set; }
    }
}
