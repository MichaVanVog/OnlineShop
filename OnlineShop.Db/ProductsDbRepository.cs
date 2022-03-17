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

        //private List<Product> products = new()
        //{
        //    new Product("Product1", 10, "Desc1", "/productImages/image1.jpg"),
        //    new Product("Product2", 20, "Desc2", "/productImages/image2.jpg"),
        //    new Product("Product3", 30, "Desc3", "/productImages/image3.jpg"),
        //    new Product("Product4", 40, "Desc4", "/productImages/image4.png"),
        //    new Product("Product5", 50, "Desc5", "/productImages/image5.jpg")
        //};

        public void Add(Product product)
        {

            product.ImagePath = "/productImages/image5.jpg";
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return databaseContext.Products.ToList();
        }

        public Product TryGetById(Guid id)
        {
            return databaseContext.Products.FirstOrDefault(product => product.Id == id);
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
    }
}
