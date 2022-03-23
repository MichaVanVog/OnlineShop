using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteDbRepository favoriteDbRepository;
        private readonly IProductsRepository productsRepository;

        public FavoriteController(IProductsRepository productsRepository, IFavoriteDbRepository favoriteDbRepository)
        {
            this.productsRepository = productsRepository;
            this.favoriteDbRepository = favoriteDbRepository;
        }

        public IActionResult Index()
        {
            var products = favoriteDbRepository.GetAll(Constants.UserId);
            return View(products.ToProductViewModels());
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            favoriteDbRepository.Add(Constants.UserId, product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(Guid productId)
        {
            favoriteDbRepository.Remove(Constants.UserId, productId);
            return RedirectToAction(nameof(Index));
        }
    }
}
