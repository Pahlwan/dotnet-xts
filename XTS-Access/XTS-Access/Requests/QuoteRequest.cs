using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XTS_Access.Domain;

namespace XTS_Access.Requests
{
    public class QuotePayLoad
    {
        public Instrument[] instruments { get; set; }
        public int xtsMessageCode { get; set; }
        public string publishFormat { get; set; }
    }
    public class QuoteRequest
    {
        private string PathFromBaseURI { get; set; } = "/instruments/quotes";
        public string URI { get; }
        private StringContent _payLoad;
        public StringContent PayLoad { get { return _payLoad; } }
        public QuoteRequest(string baseURI,Instrument[] instruments,int xtsMessageCode, string publishFormat = "JSON")
        {
            URI = baseURI + PathFromBaseURI;
            _payLoad = new StringContent(JsonConvert.SerializeObject(new QuotePayLoad() { instruments = instruments,xtsMessageCode = xtsMessageCode,publishFormat = publishFormat}), Encoding.UTF8, "application/json");
        }
    }
}
