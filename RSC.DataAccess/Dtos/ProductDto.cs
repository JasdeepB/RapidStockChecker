namespace RSC.DataAccess.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public bool InStock { get; set; }
    }
}
