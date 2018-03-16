using System.Security.Claims;
using ProductsService;
using Xunit;

namespace Tests
{
    [Trait("Category", "Unit")]
    public class ProductTests
    {
        [Fact]
        public void ProductCanReadReturnsFalseIfNoValidScopeClaim()
        {
            var claims = new[]
            {
                new Claim(ClaimSettings.Scope, "not a valid scope"),
                new Claim(ClaimSettings.UrnLocalProductId, "abc"),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var product = new Product(new ProductId("abc"));

            var result = product.CanRead(principal);

            Assert.False(result);
        }

        [Fact]
        public void ProductCanReadReturnsFalseIfNoValidProductClaim()
        {
            var claims = new[]
            {
                new Claim(ClaimSettings.Scope, ClaimSettings.ReadProduct),
                new Claim(ClaimSettings.UrnLocalProductId, "abc"),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var product = new Product(new ProductId("def"));

            var result = product.CanRead(principal);

            Assert.False(result);
        }

        // Testing successful resource access is important to verify that the
        // correct claim is needed to authorize access.  If we did not, then
        // requiring a lower claim, e.g. "read:guest" would not be caught by the
        // NoValidScopeClaim test above; This test will catch such configuration errors.
        [Fact]
        public void ProductCanReadReturnsTrueIfValidClaims()
        {
            var claims = new[]
            {
                new Claim(ClaimSettings.Scope, ClaimSettings.ReadProduct),
                new Claim(ClaimSettings.UrnLocalProductId, "abc"),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var product = new Product(new ProductId("abc"));

            var result = product.CanRead(principal);

            Assert.True(result);
        }
    }
}

