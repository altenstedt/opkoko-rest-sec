namespace ProductsService
{
    public class Product
    {
        public Product(ProductId id)
        {
            Id = id;
        }

        public ProductId Id { get; }

        public string Name => "My Product";
    }
}