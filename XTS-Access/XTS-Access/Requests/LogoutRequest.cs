using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace XTS_Access.Requests
{
    public class LogoutRequest
    {
        private string PathFromBaseURI { get; set; } = "/auth/logout";
        public string URI { get; }
        public LogoutRequest(string baseURI)
        {
            URI = baseURI + PathFromBaseURI;
        }
    }
}
