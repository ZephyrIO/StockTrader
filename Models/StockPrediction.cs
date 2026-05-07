using Microsoft.ML.Data;

namespace StockTrader.Models;

public class StockPrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedLabel { get; set; } = string.Empty;
}