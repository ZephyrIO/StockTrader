namespace StockTrader.Services
{
    public class StockPriceService(string apiKey, string tickerSymbol)
    {
        private readonly string _apiKey = apiKey;
        private readonly string _tickerSymbol = tickerSymbol;
    }
}