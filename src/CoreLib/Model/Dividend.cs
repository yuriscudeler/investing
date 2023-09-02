using Newtonsoft.Json;

namespace CoreLib.Model
{
    public class Dividend
    {
        [JsonProperty("ed")]
        public string baseDate;
        [JsonProperty("pd")]
        public string payDate;
        [JsonProperty("et")]
        public string type;
        [JsonProperty("sv")]
        public string value;

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", type, baseDate, payDate, value);
        }

        /*
        {
          "y":0,
          "m":0,
          "d":0,
          "ad":null,
          "ed":"11/07/2022",
          "pd":"18/07/2022",
          "et":"Rendimento",
          "etd":"Rendimento",
          "v":1.266000000000000000,
          "ov":null,
          "sv":"1,26600000",
          "sov":"-",
          "adj":false
        }
        */
    }
}
