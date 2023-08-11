namespace CoreLib.Model
{
    public class Stock
    {
        public string Ticker { get; set; }
        public string Url { get; set; }

        public Stock(string ticker, string url)
        {
            Ticker = ticker;
            Url = url;
        }
    }
}
