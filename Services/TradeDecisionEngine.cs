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
        if (distance > 20)
        {
            features.SMA20 = CalcSMA20(i);
        }
        if (distance > 50)
        {
            features.SMA50 = CalcSMA50(i);
        }
        if (distance > 200)
        {
            features.SMA200 = CalcSMA200(i);
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

    private float CalcSMA20(int i)
    {
        float total = 0.0f;
        for (int k = i; k < (i + 20); k++)
        {
            total += (float) _data[k].AdjClose;
        }

        float SMA20 = total / 20;
        return SMA20;
    }

    private float CalcSMA50(int i)
    {
        float total = 0.0f;
        for (int k = i; k < (i + 50); k++)
        {
            total += (float) _data[k].AdjClose;
        }

        float SMA50 = total / 50;
        return SMA50;
    }

    private float CalcSMA200(int i)
    {
        float total = 0.0f;
        for (int k = i; k < (i + 200); k++)
        {
            total += (float) _data[k].AdjClose;
        }

        float SMA200 = total / 200;
        return SMA200;
    }
}