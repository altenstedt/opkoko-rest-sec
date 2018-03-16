using System.Collections.Generic;

namespace ProductsService
{
    public class Repository : IRepository
    {
        private readonly Dictionary<ProductId, Product> dictionary = new Dictionary<ProductId, Product>
        {
            [new ProductId("abc")] = new Product(new ProductId("abc")),
            [new ProductId("def")] = new Product(new ProductId("def")),
            [new ProductId("ghi")] = new Product(new ProductId("ghi"))
        };

        public Product GetById(ProductId id)
        {
            return dictionary.GetValueOrDefault(id);
        }
    }

    public interface IRepository
    {
        Product GetById(ProductId id);
    }
}