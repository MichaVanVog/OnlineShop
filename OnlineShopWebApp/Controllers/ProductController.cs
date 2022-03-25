using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository productRepository;
        public ProductController(IProductsRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index(Guid id)
        {
            var product = productRepository.TryGetById(id);
            return View(product.ToProductViewModel());
        }
    }
}
