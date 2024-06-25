namespace StocksApp_with_Configuration_ASP.NET.Models;

public class StockTrade
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; } = 0;
    public string Country { get; set; }
    public string WebUrl { get; set; }
    public string Logo { get; set; }
    public string FinnhubIndustry { get; set; }
}
