using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;

namespace OnlineShopWebApp.Views.Shared.Components.FavoriteProducts
{
    public class FavoriteProductsViewComponent : ViewComponent
    {
        private readonly IFavoriteDbRepository favoriteDbRepository;
        public FavoriteProductsViewComponent(IFavoriteDbRepository favoriteDbRepository)
        {
            this.favoriteDbRepository = favoriteDbRepository;
        }

        public IViewComponentResult Invoke()
        {
            var productsCount = favoriteDbRepository.GetAll(Constants.UserId).Count;
            return View("FavoriteProducts", productsCount);
        }
    }
}


//TODO: решить вопрос с оторажением фаворитов. Выдает ошибку при открытие страницы. В header.cshtml уже есть ссылка