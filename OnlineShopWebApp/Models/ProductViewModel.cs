using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 1000000, ErrorMessage ="Цена должна быть в пределах от 0 до 1 000 000 руб.")]
        public decimal Cost { get; set; }

        [Required]
        public string Description { get; set; }
        public string[] ImagesPaths { get; set; }
        public string ImagePath => ImagesPaths.Length == 0 ? "/images/NoImage.png" : ImagesPaths[0];
    }
}
