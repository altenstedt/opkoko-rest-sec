using System;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsService;
using Xunit;

namespace Tests
{
    [Trait("Category", "Unit")]
    public class UnitTests
    {
        [Fact]
        public void GetProductsShouldReturn403WhenMissingClaims()
        {
            var controller = new ProductsController();

            var claims = new[] { new Claim("scope", "not a valid scope") };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var result = controller.Get();

            Assert.IsType<ForbidResult>(result);
        }

        // Testing successful resource access is important to verify that the
        // correct claim is needed to authorize access.  If we did not, then
        // requiring a lower claim, e.g. "read:guest" would not be caught by the
        // 403 test above; This test will catch such configuration errors.
        [Fact]
        public void GetProductsShouldReturn200WhenAuthorized()
        {
            var controller = new ProductsController();

            var claims = new[] { new Claim("scope", "read:product") };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var result = controller.Get();

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
