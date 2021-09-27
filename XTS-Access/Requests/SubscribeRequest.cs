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
    public class SubscriptionPayload
    {
        public Instrument[] instruments { get; set; }
        public int xtsMessageCode { get; set; }
    }
    public class SubscribeRequest
    {
        private string PathFromBaseURI { get; set; } = "instruments/subscription";
        public string URI { get; }
        private StringContent _payLoad;
        public StringContent PayLoad { get { return _payLoad; } }
        public SubscribeRequest(string baseURI, Instrument[] instruments, int xtsMessageCode)
        {
            URI = baseURI + PathFromBaseURI;
            _payLoad = new StringContent(JsonConvert.SerializeObject(new SubscriptionPayload() { instruments = instruments, xtsMessageCode = xtsMessageCode}), Encoding.UTF8, "application/json");
        }
    }
}
