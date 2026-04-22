using StockTrader.Models;

namespace StockTrader.Services;

public class TradeDecisionEngine (List<StockPrice> data)
{
    private readonly List<StockPrice> _data = data;
    private List<StockFeatures> _features = new List<StockFeatures>();

    private void CalculateFeatures(int i)
    {
        StockFeatures features = new StockFeatures();
        
        // Copy Raw OHLCV from _data
        features.AdjOpen = _data[i].AdjOpen;
        features.AdjHigh = _data[i].AdjHigh;
        features.AdjLow = _data[i].AdjLow;
        features.AdjClose = _data[i].AdjClose;
        features.Volume = (float) _data[i].Volume;
    }
}