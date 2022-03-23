using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class FavoriteDbRepository : IFavoriteDbRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavoriteDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(string userId, Product product)
        {
            var existingProduct = databaseContext.FavoriteProducts.FirstOrDefault(x => x.UserId == userId && x.Product.Id == product.Id);
            if (existingProduct == null)
            {
                databaseContext.FavoriteProducts.Add(new FavoriteProduct { Product = product, UserId = userId });
                databaseContext.SaveChanges();
            }
        }

        public void Clear (string userId)
        {
            var userFavoriteProducts = databaseContext.FavoriteProducts.Where(x => x.UserId == userId).ToList();
            databaseContext.FavoriteProducts.RemoveRange(userFavoriteProducts);
            databaseContext.SaveChanges();
        }

        public List<Product> GetAll(string userId)
        {
            return databaseContext.FavoriteProducts.Where(x => x.UserId == userId).Include(x => x.Product).Select(x => x.Product).ToList();
        }

        public void Remove(string userId, Guid productId)
        {
            var removingFavorite = databaseContext.FavoriteProducts.FirstOrDefault(x => x.UserId == userId && x.Product.Id == productId);
            databaseContext.FavoriteProducts.Remove(removingFavorite);
            databaseContext.SaveChanges();
        }
    }
}
