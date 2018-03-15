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

            var claims = new[] { new Claim("urn:my:claim", "42") };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var result = controller.Get();

            Assert.IsType<ForbidResult>(result);
        }
    }
}
