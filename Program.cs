using Microsoft.Extensions.Configuration;
using StockTrader.Models;
using StockTrader.Services;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

string? apiKey = config["StockApiKey"];

Console.Write("Input the Ticker Symbol (e.g. AAPL for Apple) for the stock you wish to evaluate: ");
string? tickerSymbol = Console.ReadLine();

var stockPriceService = new StockPriceService(apiKey, tickerSymbol);
List<StockPrice> data = await stockPriceService.GetStockPricesAsync();

Console.WriteLine("data.[0].AdjOpen = " + data[0].AdjOpen);