using Microsoft.AspNetCore.Mvc;

namespace ProductsService
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IAuthorizationServiceAdapter _authorizationServiceAdapter;
        private readonly IProductsService _productsService;

        public ProductsController(IAuthorizationServiceAdapter authorizationServiceAdapter, IProductsService productsService)
        {
            _authorizationServiceAdapter = authorizationServiceAdapter;
            _productsService = productsService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            if (!ProductId.IsValidId(id))
            {
                return BadRequest(); // https://stackoverflow.com/q/3290182/291299
            }

            var productId = new ProductId(id);

            var product = _productsService.GetById(productId);

            if (product == null)
            {
                return NotFound();
            }

            if (!_authorizationServiceAdapter.CanRead(product, User))
            {
                return Forbid();
            }

            return Ok(product);
        }
    }
}