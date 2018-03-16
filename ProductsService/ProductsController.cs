using Microsoft.AspNetCore.Mvc;

namespace ProductsService
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IAuthorizationServiceAdapter _authorizationServiceAdapter;
        private readonly IRepository _repository;

        public ProductsController(IAuthorizationServiceAdapter authorizationServiceAdapter, IRepository repository)
        {
            _authorizationServiceAdapter = authorizationServiceAdapter;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            if (!ProductId.IsValidId(id))
            {
                return BadRequest(); // https://stackoverflow.com/q/3290182/291299
            }

            var productId = new ProductId(id);

            var product = _repository.GetById(productId);

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