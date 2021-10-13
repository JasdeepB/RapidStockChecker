using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSC.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string SKU { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Retailer { get; set; }
        public bool InStock { get; set; }
        public virtual RSC.Models.Type Type { get; set; }
        public virtual Discord Discord { get; set; }
    }
}
