namespace StockTrader.Models;

public class StockFeatures
{
    // Raw OHLCV Values
    public float AdjOpen { get; set; }
    public float AdjHigh { get; set; }
    public float AdjLow { get; set; }
    public float AdjClose { get; set; }
    public float Volume { get; set; }

    // Trend Indicators
    public float SMA10 { get; set; }
    public float SMA20 { get; set; }
    public float SMA50 { get; set; }
    public float SMA200 { get; set; }
    public float EMA12 { get; set; }
    public float EMA26 { get; set; }
    public float MACD { get; set; }
    public float MACDSignal { get; set; }

    // Momentum Indicators
    public float RSI14 { get; set; }
    public float ROC10 { get; set; }

    // Volatility Indicators
    public float ATR14 { get; set; }
    public float BollingerUpper { get; set; }
    public float BollingerMiddle { get; set; }
    public float BollingerLower { get; set; }

    // Volume Indicators
    public float OBV { get; set; }
    public float VolumeSMA20 { get; set; }

    // Label (Buy/Sell/Hold) - used during training, ignored during prediction
    public string Label { get; set; } = "Hold";
}