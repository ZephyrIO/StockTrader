using System.Text.Json;
using StockTrader.Models;

namespace StockTrader.Services;
public class StockPriceService(string apiKey, string tickerSymbol)
{
    private readonly string _apiKey = apiKey;
    private readonly string _tickerSymbol = tickerSymbol;
    private readonly HttpClient _httpClient = new();

    public async Task<List<StockPrice>> GetStockPricesAsync()
    {
        string url = $"https://financialmodelingprep.com/stable/historical-price-eod/dividend-adjusted?symbol={_tickerSymbol}&apikey={_apiKey}";
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        List<StockPrice>? prices = JsonSerializer.Deserialize<List<StockPrice>>(json,
        new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
        
        return prices ?? [];
    }
}