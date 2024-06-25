using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Services;
using StocksApp_with_Configuration_ASP.NET.Models;

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

        // create model object
        StockTrade stockTrade = new()
        {
            Symbol = _tradingOptions.DefaultStockSymbol
        };

        // Load data from FinnhubService into model object
        if (companyProfile != null && stockPriceQuote != null)
            stockTrade = new StockTrade()
            {
                Symbol = Convert.ToString(companyProfile["ticker"]),
                Name = Convert.ToString(companyProfile["name"]),
                Price = Convert.ToDouble(stockPriceQuote["c"].ToString()),
                Logo = Convert.ToString(companyProfile["logo"]),
                Country = Convert.ToString(companyProfile["country"]),
                WebUrl = Convert.ToString(companyProfile["weburl"]),
                FinnhubIndustry =
                    Convert.ToString(companyProfile["finnhubIndustry"])
            };

        // Send Finnhub token to view
        ViewBag.FinnhubToken = _configuration["FinnhubToken"];

        return View(stockTrade);
    }
}
