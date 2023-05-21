using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLib.Model
{
    public class Stock
    {
        public string ticker;
        public string url;

        public Stock(string ticker, string url)
        {
            this.ticker = ticker;
            this.url = url;
        }
    }
}
