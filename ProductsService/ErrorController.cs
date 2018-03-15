using System;
using Microsoft.AspNetCore.Mvc;

namespace ProductsService
{
    [Route("error")]
    public class ErrorController
    {
        [HttpPost]
        public IActionResult Throw()
        {
            throw new InvalidOperationException("This message might contain sensitive information.");
        }
    }
}