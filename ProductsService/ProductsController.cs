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
        private readonly Dictionary<string, Product> repository = new Dictionary<string, Product>
        {
            ["abc"] = new Product("abc")
        };

        [HttpGet]
        public IActionResult GetById(string id)
        {
            if (!ProductId.IsValidId(id))
            {
                return BadRequest();
            }

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
            Id = new ProductId(id);
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
            AssertId(id);

            Value = id;
        }

        public string Value { get; }

        public static bool IsValidId(string id)
        {
            return id.All(char.IsLetterOrDigit); // Regex.IsMatch(id, "^[a-f0-9]+$");
        }

        public static void AssertId(string id)
        {
            if (!IsValidId(id))
            {
                throw new ArgumentException($"Id {id} is not valid.");
            }
        }
    }
}