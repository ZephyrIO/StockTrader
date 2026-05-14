using StockTrader.Models;

namespace StockTrader.Services;

public static class StockDecideService
{
    public static async Task<string> CreateStockDecisionListAsync(string apiKey, string tickerSymbol)
    {
        var stockPriceService = new StockPriceService(apiKey, tickerSymbol);
        List<StockPrice> data = await stockPriceService.GetStockPricesAsync();

        var featureEngine = new FeatureEngine(data);
        featureEngine.CreateFeatures();
        featureEngine.LabelFeatures();

        var decisionModel = new DecisionEngine(featureEngine.Features);
        return decisionModel.TrainAndPredict();
    }
}