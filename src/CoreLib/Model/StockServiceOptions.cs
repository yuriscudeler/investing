using System;
using System.Collections.Generic;

namespace CoreLib.Model
{
    public class StockServiceOptions
    {
        public const string Key = "StockService";
        public List<Stock> Stocks { get; set; }
        public DateTime TimeToRun { get; set; }
        public string LogDirectory { get; set; }
        public string DataDirectory { get; set; }
    }
}
