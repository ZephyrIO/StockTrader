using StockTrader.Models;

namespace StockTrader.Services;

public class TradeDecisionEngine (List<StockPrice> data)
{
    private readonly List<StockPrice> _data = data;
    private List<StockFeatures> _features = new List<StockFeatures>();

    private void CalculateFeatures(int pos)
    {
        StockFeatures features = new StockFeatures();

        // Copy Raw OHLCV from _data
        features.AdjOpen = (float) _data[pos].AdjOpen;
        features.AdjHigh = (float) _data[pos].AdjHigh;
        features.AdjLow = (float) _data[pos].AdjLow;
        features.AdjClose = (float) _data[pos].AdjClose;
        features.Volume = (float) _data[pos].Volume;

        // Determine number of elements between current element and oldest element
        int distance = _data.Count - pos;

        // If distance is greater than the number of elements needed to calculate the value, calculate the value
        if (distance >= 10)
        {
            features.SMA10 = CalcSMA(pos, 10);
        }
        if (distance >= 20)
        {
            features.SMA20 = CalcSMA(pos, 20);
        }
        if (distance >= 50)
        {
            features.SMA50 = CalcSMA(pos, 50);
        }
        if (distance >= 200)
        {
            features.SMA200 = CalcSMA(pos, 200);
        }
    }

    private float CalcSMA(int pos, int window)
    {
        float total = 0.0f;
        for (int k = pos; k < (pos + window); k++)
        {
            total += (float) _data[k].AdjClose;
        }

        float SMA10 = total / window;
        return SMA10;
    }
}