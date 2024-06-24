using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceContracts;

namespace Services;

public class FinnhubService : IFinnhubService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public FinnhubService(IHttpClientFactory httpClientFactory,
                          IConfiguration configuration
    )
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<Dictionary<string, object>?> GetCompanyProfile(
        string stockSymbol
    )
    {
        using (HttpClient httpClient = _httpClientFactory.CreateClient())
        {
            HttpRequestMessage httpRequestMessage = new()
            {
                RequestUri =
                    new Uri(
                        $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
                Method = HttpMethod.Get
            };

            HttpResponseMessage httpResponseMessage =
                await httpClient.SendAsync(httpRequestMessage);

            string responseBody =
                new StreamReader(httpResponseMessage.Content.ReadAsStream())
                    .ReadToEnd();

            Dictionary<string, object>? responseDictionary =
                JsonSerializer
                    .Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException(
                    "No response from finnhub server!");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(
                    Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }
    }

    public Task<Dictionary<string, object>?> GetStockPriceQuote(
        string stockSymbol
    )
    {
        throw new NotImplementedException();
    }
}
