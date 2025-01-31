using Microsoft.AspNetCore.Mvc;

namespace EFoodCommerce.Areas.Error.Controllers
{
    [Area("Error")]
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            return statusCode switch
            {
                404 => View("NotFound"),
                500 => View("ServerError"),
                _ => View("Index"),
            };
        }

    }
}