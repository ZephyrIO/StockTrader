using StockTrader.Models;

namespace StockTrader.Services;

public class TradeDecisionEngine
{
    private readonly List<StockPrice> _data;
    private List<StockFeatures> _features = new List<StockFeatures>();

    public TradeDecisionEngine(List<StockPrice> data)
    {
        _data = data;
        _data.Reverse();
    }


    private StockFeatures? CalculateFeatures(int pos)
    {
        if (pos < 199)
        {
            return null;
        }

        StockFeatures features = new StockFeatures();

        // Copy Raw OHLCV from _data
        features.AdjOpen = (float) _data[pos].AdjOpen;
        features.AdjHigh = (float) _data[pos].AdjHigh;
        features.AdjLow = (float) _data[pos].AdjLow;
        features.AdjClose = (float) _data[pos].AdjClose;
        features.Volume = (float)_data[pos].Volume;

        features.SMA10 = CalcSMA(pos, 10);
        features.SMA20 = CalcSMA(pos, 20);
        features.SMA50 = CalcSMA(pos, 50);
        features.SMA200 = CalcSMA(pos, 200);

        // Calculate remaining derived values
        return features;
    }

    private float CalcSMA(int pos, int window)
    {
        float total = 0.0f;
        for (int k = pos; k < (pos - window); k--)
        {
            total += (float) _data[k].AdjClose;
        }

        float SMA10 = total / window;
        return SMA10;
    }
}