using System.ComponentModel.DataAnnotations;

namespace WebBanDienThoai.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } 
        public string Category { get; set; }
        public string Description { get; set; }
    }
}