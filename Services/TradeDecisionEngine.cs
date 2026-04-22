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
        features.AdjOpen = (float) _data[i].AdjOpen;
        features.AdjHigh = (float) _data[i].AdjHigh;
        features.AdjLow = (float) _data[i].AdjLow;
        features.AdjClose = (float) _data[i].AdjClose;
        features.Volume = (float) _data[i].Volume;

        // Determine number of elements between current element and oldest element
        int distance = _data.Count - i;

        // If distance is greater than the number of elements needed to calculate the value, calculate the value
        if (distance > 10)
        {
            features.SMA10 = CalcSMA10(i);
        }
    }

    private float CalcSMA10(int i)
    {
        float total = 0.0f;
        for (int k = i; k < (i + 10); k++)
        {
            total += (float) _data[k].AdjClose;
        }

        float SMA10 = total / 10;
        return SMA10;
    }
}