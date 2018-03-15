using System;
using ProductsService;
using Xunit;

namespace Tests
{
    [Trait("Category", "Unit")]
    public class UnitTest1
    {
        [Theory]
        [InlineData("<script>")]
        [InlineData("'1==1")]
        [InlineData("--sql")]
        public void ProductIdShouldRejectInjection(string id)
        {
            Assert.False(ProductId.IsValidId(id));
        }

        [Theory]
        [InlineData("<script>")]
        [InlineData("'1==1")]
        [InlineData("--sql")]
        public void ProductIdConstructorShouldThrowOnInjection(string id)
        {
            Assert.Throws<ArgumentException>(() => new ProductId(id));
        }
    }
}
