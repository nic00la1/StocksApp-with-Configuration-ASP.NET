namespace ServiceContracts;

public interface IFinnhubService
{
    // Returns a dictionary that contains details such as company country, currency exchange, 
    // IPO date, market capitalization, and more.
    Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
}
