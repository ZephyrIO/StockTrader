using StockTrader.Models;

namespace StockTrader.Services;

public class TradeDecisionEngine (List<StockPrice> data)
{
    private readonly List<StockPrice> _data = data;
    private readonly List<StockFeatures> _features = new List<StockFeatures>();
}