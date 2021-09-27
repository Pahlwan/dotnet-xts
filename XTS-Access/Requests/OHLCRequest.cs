using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XTS_Access.Requests
{
    public class OHLCRequest
    {
        private string PathFromBaseURI { get; set; } = "/instruments/quotes";
        public string URI { get; }
        public OHLCRequest(string baseURI, int exchangeSegment, int exchangeInstrumentID, DateTime startDateTime,DateTime endDateTime,int candleInterval)
        {
            URI = baseURI + $"/instruments/ohlc?exchangeSegment={exchangeSegment}&exchangeInstrumentID={exchangeInstrumentID}&startTime={startDateTime.ToString("MMM dd yyyy HHmmss")}&endTime={endDateTime.ToString("MMM dd yyyy HHmmss")}&compressionValue={candleInterval*60}";
        }
    }
}
