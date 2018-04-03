using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using ProductsService;
using Xunit;
using Moq;

namespace Tests
{
    [Trait("Category", "Unit")]
    public class ProductsControllerTests
    {
        [Fact]
        public void GetProductsByIdShouldReturn403WhenCanNotRead()
        {
            var productServiceMock = new Mock<IProductsService>();
            productServiceMock.Setup(ps => ps.GetById(It.IsAny<IPrincipal>(), It.IsAny<ProductId>())).Returns(new ProductResult(ServiceResult.Forbidden, null));

            var controller = new ProductsController(productServiceMock.Object);

            var result = controller.GetById("abc");

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public void GetProductsByIdShouldReturn200WhenAuthorized()
        {
            var productServiceMock = new Mock<IProductsService>();
            productServiceMock.Setup(ps => ps.GetById(It.IsAny<IPrincipal>(), It.IsAny<ProductId>())).Returns(new ProductResult(ServiceResult.Ok, new Product(new ProductId("abc"))));

            var controller = new ProductsController(productServiceMock.Object);

            var result = controller.GetById("abc");

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(IdInjection))]
        [MemberData(nameof(InvalidIds))]
        public void GetProductsByIdShouldReturn400WhenInvalidId(string id)
        {
            var productServiceMock = new Mock<IProductsService>();
            productServiceMock.Setup(ps => ps.GetById(It.IsAny<IPrincipal>(), It.IsAny<ProductId>())).Returns(new ProductResult(ServiceResult.Ok, new Product(new ProductId("abc"))));

            var controller = new ProductsController(productServiceMock.Object);

            var result = controller.GetById(id);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetProductsByIdShouldReturn404WhenNotFound()
        {
            var productServiceMock = new Mock<IProductsService>();
            productServiceMock.Setup(ps => ps.GetById(It.IsAny<IPrincipal>(), It.IsAny<ProductId>())).Returns(new ProductResult(ServiceResult.NotFound, null));
            
            var controller = new ProductsController(productServiceMock.Object);

            var result = controller.GetById("def"); // This is a valid, non-existing id

            Assert.IsType<NotFoundResult>(result);
        }

        public static IEnumerable<object[]> IdInjection => new[]
        {
            new object[] { "<script>" },
            new object[] { "'1==1" },
            new object[] { "--sql" }
        };

        public static IEnumerable<object[]> InvalidIds => new[]
        {
            new object[] { "" },
            new object[] { "no spaces" },
            new object[] { "thisisanidthatistoolong" },
            new object[] { "#" }
        };
    }
}
