using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ImagesProvider imagesProvider;

        public ProductController(IProductsRepository productsRepository, ImagesProvider imagesProvider)
        {
            this.productsRepository = productsRepository;
            this.imagesProvider = imagesProvider;
        }

        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(products.ToProductViewModels());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var imagesPaths = imagesProvider.SafeFiles(product.UploadedFiles, ImageFolders.Products);
            productsRepository.Add(product.ToProduct(imagesPaths));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            return View(product.ToEditProductViewModel());
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var imagesPaths = imagesProvider.SafeFiles(product.UploadedFiles, ImageFolders.Products);
            product.ImagesPaths.AddRange(imagesPaths);
            productsRepository.Update(product.ToProduct());
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Remove(Guid productId)
        //{
        //    productsRepository.Remove(productId);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
