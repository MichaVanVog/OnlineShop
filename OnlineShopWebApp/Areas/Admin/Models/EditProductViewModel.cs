using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 1000000, ErrorMessage = "Цена должна быть в пределах от 0 до 1 000 000 руб.")]
        public decimal Cost { get; set; }

        [Required]
        public string Description { get; set; }

        public List<string> ImagesPaths { get; set; }

        public FormFile[] UploadedFiles { get; set; }
    }
}
