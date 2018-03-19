using System.Collections.Generic;

namespace ProductsService
{
    public class ProductsService : IProductsService
    {
        private readonly Dictionary<ProductId, Product> _productRepository = new Dictionary<ProductId, Product>
        {
            [new ProductId("abc")] = new Product(new ProductId("abc")),
            [new ProductId("def")] = new Product(new ProductId("def")),
            [new ProductId("ghi")] = new Product(new ProductId("ghi"))
        };

        public Product GetById(ProductId id)
        {
            return _productRepository.GetValueOrDefault(id);
        }
    }

    public interface IProductsService
    {
        Product GetById(ProductId id);
    }
}