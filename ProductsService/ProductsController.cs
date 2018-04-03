using Microsoft.AspNetCore.Mvc;

namespace ProductsService
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
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

            var productResult = _productsService.GetById(User, productId);

            if (productResult.Result == ServiceResult.NotFound)
            {
                return NotFound();
            }

            if (productResult.Result == ServiceResult.Forbidden)
            {
                return Forbid();
            }

            return Ok(productResult.Value);
        }
    }
}