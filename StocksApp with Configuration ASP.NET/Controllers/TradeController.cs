using Microsoft.AspNetCore.Mvc;

namespace StocksApp_with_Configuration_ASP.NET.Controllers;

public class TradeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
}
