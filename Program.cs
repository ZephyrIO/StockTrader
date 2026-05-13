using Microsoft.Extensions.Configuration;
using StockTrader.Models;
using StockTrader.Services;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();
string? apiKey = config["StockApiKey"];

string[] tickers = ["AAPL", "TSLA", "AMZN", "MSFT", "NVDA", "GOOGL", "META", "NFLX", "JPM", "V", "BAC", "PYPL", "DIS", "T", "PFE", "COST", "INTC", "KO", "TGT",
    "NKE", "SPY", "BA", "BABA", "XOM", "WMT", "GE", "CSCO", "VZ", "JNJ", "CVX", "PLTR", "SHOP", "SBUX", "SOFI", "HOOD", "RBLX", "SNAP", "AMD", "UBER", "FDX",
    "ABBV", "ETSY", "MRNA", "LMT", "GM", "F", "LCID", "CCL", "DAL", "UAL", "AAL", "TSM", "SONY", "ET", "COIN", "RIVN", "RIOT", "CPRX", "VWO", "SPYG", "NOK",
    "ROKU", "BIDU", "DOCU", "ZM", "PINS", "TLRY", "MGM", "NIO", "C", "GS", "WFC", "ADBE", "PEP", "UNH", "CARR", "HCA", "BILI", "SIRI", "FUBO", "RKT"];

List<string> results = ["ticker,decision"];
for (int i = 0; i < tickers.Length; i++)
{
    string decision = await StockDecideService.CreateStockDecisionListAsync(apiKey, tickers[i]);
    results.Add($"{tickers[i]},{decision}");
}

string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "decisions.csv");
await File.WriteAllLinesAsync(csvPath, results);
Console.WriteLine($"Results saved to {csvPath}");