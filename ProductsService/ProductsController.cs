using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace ProductsService
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly Dictionary<ProductId, Product> repository = new Dictionary<ProductId, Product>
        {
            [new ProductId("abc")] = new Product(new ProductId("abc"))
        };

        [HttpGet]
        public IActionResult GetById(string id)
        {
            if (!ProductId.IsValidId(id))
            {
                return BadRequest(); // https://stackoverflow.com/q/3290182/291299
            }

            var productId = new ProductId(id);

            var product = repository.GetValueOrDefault(productId);

            if (product == null)
            {
                return NotFound();
            }

            if (!product.CanRead(User))
            {
                return Forbid();
            }

            return Ok(product);
        }
    }

    public class Product
    {
        public Product(ProductId id)
        {
            Id = id;
        }

        public ProductId Id { get; }

        public string Name => "My Product";

        public bool CanRead(ClaimsPrincipal principal)
        {
            return principal.HasClaim(c => c.Type == "scope" && c.Value.Contains("read:product"));
        }
    }

    public class ProductId
    {
        public ProductId(string id)
        {
            AssertValidId(id);

            Value = id;
        }

        public string Value { get; }

        public static bool IsValidId(string id)
        {
            return !string.IsNullOrEmpty(id) && id.Length < 10 && id.All(char.IsLetterOrDigit);
        }

        public static void AssertValidId(string id)
        {
            if (!IsValidId(id))
            {
                throw new ArgumentException($"Id {id} is not valid.");
            }
        }

        public static bool operator ==(ProductId left, ProductId right)
        {
            if (ReferenceEquals(null, left))
            {
                return ReferenceEquals(null, right);
            }

            if (ReferenceEquals(null, right))
            {
                return false;
            }

            if (ReferenceEquals(left, right))
            {
                return true;
            }

            return string.Equals(left.Value, right.Value, StringComparison.Ordinal);
        }

        public static bool operator !=(ProductId left, ProductId right)
        {
            return !(left == right);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return (ProductId)obj == this; // This works since we also override the == operator.
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 23;
                hash = hash * 31 + Value.GetHashCode();

                return hash;
            }
        }
    }
}