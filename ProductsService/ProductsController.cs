using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ProductsService
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly Dictionary<string, Product> repository = new Dictionary<string, Product>
        {
            ["abc"] = new Product("abc")
        };

        [HttpGet]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id) || !repository.ContainsKey(id))
            {
                return NotFound();
            }

            var product = repository[id];

            if (!product.CanRead(User))
            {
                return Forbid();
            }

            return Ok(product);
        }
    }

    public class Product
    {
        public Product(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public string Name => "My Product";

        public bool CanRead(ClaimsPrincipal principal)
        {
            return principal.HasClaim(c => c.Type == "scope" && c.Value.Contains("read:product"));
        }
    }
}