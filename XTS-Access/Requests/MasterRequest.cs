using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XTS_Access.Requests
{
    class MasterRequestPayload
    {
        public string[] exchangeSegmentList;
    }

    public class MasterRequest
    {
        private string PathFromBaseURI { get; set; } = "/instruments/master";
        public string URI { get; }
        private StringContent _payLoad;
        public StringContent PayLoad { get { return _payLoad; } }
        public MasterRequest(string baseURI,string[] exchangeSegmentList)
        {
            URI = baseURI + PathFromBaseURI;
            _payLoad = new StringContent(JsonConvert.SerializeObject(new MasterRequestPayload() { exchangeSegmentList = exchangeSegmentList }), Encoding.UTF8, "application/json");
        }
    }
}
