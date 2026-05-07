using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.TimeSeries;
using Microsoft.ML.Trainers.FastTree;
using StockTrader.Models;

namespace StockTrader.Services;

public class DecisionEngine (List<StockFeatures> features)
{
    private readonly List<StockFeatures> _features = features;
    private readonly MLContext _mlContext = new MLContext(seed: 0);

    public string TrainAndPredict()
    {
        // Load the features list into an ML.NET data view
        IDataView dataView = _mlContext.Data.LoadFromEnumerable(_features);

        // Define the feature columns that the model will learn from
        string[] featureColumns = [
            nameof(StockFeatures.AdjOpen),
            nameof(StockFeatures.AdjHigh),
            nameof(StockFeatures.AdjLow),
            nameof(StockFeatures.AdjLow),
            nameof(StockFeatures.AdjClose),
            nameof(StockFeatures.Volume),
            nameof(StockFeatures.SMA10),
            nameof(StockFeatures.SMA20),
            nameof(StockFeatures.SMA50),
            nameof(StockFeatures.SMA200),
            nameof(StockFeatures.EMA12),
            nameof(StockFeatures.EMA26),
            nameof(StockFeatures.MACD),
            nameof(StockFeatures.MACDSignal),
            nameof(StockFeatures.RSI14),
            nameof(StockFeatures.ROC10),
            nameof(StockFeatures.ATR14),
            nameof(StockFeatures.BollingerUpper),
            nameof(StockFeatures.BollingerMiddle),
            nameof(StockFeatures.BollingerLower),
            nameof(StockFeatures.OBV),
            nameof(StockFeatures.VolumeSMA20)
        ];

        // Define the training pipeline
        var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label")
            .Append(_mlContext.Transforms.Concatenate("Features", featureColumns))
            .Append(_mlContext.MulticlassClassification.Trainers.OneVersusAll(
                _mlContext.BinaryClassification.Trainers.FastTree(
                    numberOfLeaves: 20,
                    numberOfTrees: 100,
                    minimumExampleCountPerLeaf: 5)))
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
    }

}