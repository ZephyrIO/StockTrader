using StockTrader.Models;

namespace StockTrader.Services;

public class TradeDecisionEngine (List<StockPrice> data)
{
    private readonly List<StockPrice> _data = data;
}