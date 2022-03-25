using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return databaseContext.Products.Include(x => x.Images).ToList();
        }

        public Product TryGetById(Guid id)
        {
            return databaseContext.Products.Include(x=>x.Images).FirstOrDefault(product => product.Id == id);
        }

        public void Update(Product product)
        {
            var existingProduct = databaseContext.Products.FirstOrDefault(product => product.Id == product.Id);
            if (existingProduct == null)
            {
                return;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Cost = product.Cost;
            databaseContext.SaveChanges();
        }

        //public void Remove(Guid id)
        //{
        //    var product = TryGetById(id);
        //    databaseContext.Products.Remove(product);
        //    databaseContext.SaveChanges();
        //}
    }
}
