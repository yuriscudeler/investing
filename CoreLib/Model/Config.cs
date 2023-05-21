using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLib.Model
{
    public class Config
    {
        public List<Stock> stocks;
        public DateTime timeToRun;

        public Config()
        {
            // default config
            stocks = new List<Stock>();
            timeToRun = DateTime.Parse("06:00:00");
        }

        public static Config FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Config>(json);
        }
    }
}
