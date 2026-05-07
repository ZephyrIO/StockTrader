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
    {}

}