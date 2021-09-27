using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XTS_Access.Requests
{
    public class LoginPayLoad
    {
        public string secretKey { get; set; }
        public string appKey { get; set; }
        public string source { get; set; } 
    }
    public class LoginRequest
    {
        private string _secrateKey;
        private string _apiKey;
        private string PathFromBaseURI { get; set; } = "/auth/login";
        public string URI { get; }
        private StringContent _payLoad;
        public StringContent PayLoad { get { return _payLoad; } }
        public LoginRequest(string baseURI,string secrateKey,string apiKey)
        {
            _secrateKey = secrateKey;
            _apiKey = apiKey;
            URI = baseURI + PathFromBaseURI;
            GenetatePayLoad();
        }
        private void GenetatePayLoad()
        {
            _payLoad = new StringContent(JsonConvert.SerializeObject(new LoginPayLoad() { secretKey = _secrateKey, appKey = _apiKey, source = "WebAPI" }), Encoding.UTF8, "application/json");
        }
    }
}
