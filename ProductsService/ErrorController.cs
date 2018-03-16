using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsService
{
    [AllowAnonymous]
    [Route("error")]
    public class ErrorController
    {
        [HttpGet]
        public IActionResult Throw()
        {
            throw new InvalidOperationException("This message might contain sensitive information.");
        }
    }
}