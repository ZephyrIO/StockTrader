namespace StockTrader.Models
{
    public class StockPrice
    {
        public required string Symbol { get; set; }
        public required string Date { get; set; }
        public double AdjOpen { get; set; }
        public double AdjHigh { get; set; }
        public double AdjLow { get; set; }
        public double AdjClose { get; set; }
        public long Volume { get; set; }
    }
}