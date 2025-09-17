using ProductService.Models;

namespace ProductService.Repository
{
    public class ProductRepository
    {
        private readonly ProductDBContext dbContext;

        public ProductRepository(ProductDBContext context)
        {
            dbContext = context;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> listOfProducts = dbContext.Products.ToList();
            return listOfProducts;
        }

        public bool AddNewProduct(Product product)
        {
            bool status;
            try
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public int UpdateProductDetails(Product product)
        {
            int status = -1;
            Product productObj = dbContext.Products.Find(product.ProductId);
            try
            {
                if (productObj != null)
                {
                    productObj.ProductName = product.ProductName;
                    productObj.CategoryId = product.CategoryId;
                    productObj.Price = product.Price;
                    productObj.QuantityAvailable = product.QuantityAvailable;
                    dbContext.Products.Update(productObj);
                    dbContext.SaveChanges();
                    status = 1;
                }
            }
            catch (Exception)
            {
                status = -99;
            }
            return status;
        }

        public bool DeleteProduct(string productId)
        {
            bool status = false;
            Product productObj = dbContext.Products.Find(productId);
            try
            {
                if (productObj != null)
                {
                    dbContext.Products.Remove(productObj);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
