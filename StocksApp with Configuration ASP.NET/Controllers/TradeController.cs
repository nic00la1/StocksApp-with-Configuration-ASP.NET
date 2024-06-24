using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Services;

namespace StocksApp_with_Configuration_ASP.NET.Controllers;

public class TradeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly TradingOptions _tradingOptions;
    private readonly IFinnhubService _finnhubService;

    public TradeController(IOptions<TradingOptions> tradingOptions,
                           IFinnhubService finnhubService,
                           IConfiguration configuration
    )
    {
        _tradingOptions = tradingOptions.Value;
        _finnhubService = finnhubService;
        _configuration = configuration;
    }

    [Route("/")]
    public IActionResult Index()
    {
        // reset stock symbol if not exists
        if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
            _tradingOptions.DefaultStockSymbol = "MSFT";

        // get company profile from API server
        Dictionary<string, object>? companyProfile =
            _finnhubService
                .GetCompanyProfile(_tradingOptions.DefaultStockSymbol)
                .Result;

        // get stock price quotes from API server
        Dictionary<string, object>? stockPriceQuote =
            _finnhubService
                .GetStockPriceQuote(_tradingOptions.DefaultStockSymbol)
                .Result;


        return View();
    }
}
