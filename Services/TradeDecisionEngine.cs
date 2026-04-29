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

    public void CreateFeatures()
    {
        for (int i = 0; i < _data.Count; i++)
        {
            var stockFeature = CalculateFeatures(i);
            if (stockFeature != null)
            {
                _features.Add(stockFeature);
            }
        }
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

        features.EMA12 = CalcEMA(pos, 12);
        features.EMA26 = CalcEMA(pos, 26);

        features.MACD = CalcMACD(pos);
        features.MACDSignal = CalcMACDSignal(pos);

        features.RSI14 = CalcRSI(pos);
        features.ROC10 = CalcROC(pos);

        features.ATR14 = CalcATR(pos);

        float bollingerWidth = CalcBollinger(pos, features.SMA20);
        features.BollingerMiddle = features.SMA20;
        features.BollingerUpper = features.BollingerMiddle + bollingerWidth;
        features.BollingerLower = features.BollingerMiddle - bollingerWidth;

        features.OBV = CalcOBV(pos);
        features.VolumeSMA20 = CalcVSMA(pos);

        return features;
    }

    private float CalcSMA(int pos, int window)
    {
        float total = 0.0f;

        for (int k = pos; k > (pos - window); k--)
        {
            total += (float) _data[k].AdjClose;
        }

        float sma = total / window;
        return sma;
    }

    private float CalcEMA(int pos, int window)
    {
        float multiplier = 2.0f / (window + 1);
        float ema = CalcSMA(pos - (window - 1), window);

        for (int k = pos - (window - 2); k <= pos; k++)
        {
            ema = (((float)_data[k].AdjClose - ema) * multiplier) + ema;
        }

        return ema;
    }

    private float CalcMACD(int pos)
    {
        float EMA12 = CalcEMA(pos, 12);
        float EMA26 = CalcEMA(pos, 26);
        return EMA12 - EMA26;
    }

    private float CalcMACDSignal(int pos)
    {
        const float multiplier = 2.0f / (9 + 1);
        float macdSignal = CalcMACD(pos - (9 - 1));

        for (int k = pos - (9 - 2); k <= pos; k++)
        {
            macdSignal = ((CalcMACD(k) - macdSignal) * multiplier) + macdSignal;
        }

        return macdSignal;
    }

    private float CalcRSI(int pos)
    {
        float avgGain = 0.0f;
        float avgLoss = 0.0f;

        for (int i = (pos - 13); i <= pos; i++)
        {
            float change = (float)_data[i].AdjClose - (float)_data[i - 1].AdjClose;
            if (change > 0)
            {
                avgGain += change;
            }
            else
            {
                avgLoss += Math.Abs(change);
            }
        }

        avgGain /= 14;
        avgLoss /= 14;

        if (avgLoss == 0) return 100;
        float RS = avgGain / avgLoss;
        float RSI = 100 - (100 / (1 + RS));
        return RSI;
    }

    private float CalcROC(int pos)
    {
        float ROC = ((float)_data[pos].AdjClose - (float)_data[pos - 10].AdjClose) / (float)_data[pos - 10].AdjClose;
        ROC *= 100;
        return ROC;
    }

    private float CalcATR(int pos)
    {
        float ATR = 0.0f;
        for (int i = (pos - 13); i <= pos; i++)
        {
            float trueRange = (float)Math.Max(_data[i].AdjHigh - _data[i].AdjLow, Math.Max(Math.Abs(_data[i].AdjHigh - _data[i - 1].AdjClose),
            Math.Abs(_data[i].AdjLow - _data[i - 1].AdjClose)));

            ATR += trueRange;
        }

        ATR /= 14;
        return ATR;
    }

    private float CalcBollinger(int pos, float SMA)
    {
        double total = 0.0f;
        for (int i = pos - 19; i <= pos; i++)
        {
            total += Math.Pow((float)_data[i].AdjClose - SMA, 2);
        }

        float stdDev = (float) Math.Sqrt(total / 20);
        return stdDev * 2;
    }

    private float CalcOBV(int pos)
    {
        if (pos == 199)
        {
            if (_data[pos].AdjClose > _data[pos - 1].AdjClose)
            {
                return (float) _data[pos].Volume;
            }
            else if (_data[pos].AdjClose < _data[pos - 1].AdjClose)
            {
                return (float) 0 - _data[pos].Volume;
            }
            else
            {
                return 0.0f;
            }
        }
        else
        {
            if (_data[pos].AdjClose > _data[pos - 1].AdjClose)
            {
                return _features[^1].OBV + (float) _data[pos].Volume;
            }
            else if (_data[pos].AdjClose < _data[pos - 1].AdjClose)
            {
                return _features[^1].OBV - (float) _data[pos].Volume;
            }
            else
            {
                return _features[^1].OBV;
            }
        }
    }

    private float CalcVSMA(int pos)
    {
        float total = 0.0f;

        for (int k = pos; k > (pos - 20); k--)
        {
            total += (float) _data[k].Volume;
        }

        float vSMA = total / 20;
        return vSMA;
    }
}