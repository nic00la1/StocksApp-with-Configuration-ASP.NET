namespace StocksApp_with_Configuration_ASP.NET.Models;

public class StockTrade
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; } = 0;
    public uint Quantity { get; set; } = 0;
}
