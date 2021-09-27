using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTS_Access.Response
{
    public class OHLCResult
    {
        public int exchangeSegment;// "NSECM",
        public string exchangeInstrumentID;// "22",
        public string dataReponse;

    }
    public class OHLCResponse
    {
        public string type;// "success",
        public string code;// "s-ohlc-0001",
        public string description;// "Get Ohlc successfully!",
        public OHLCResult result;
    }
}
