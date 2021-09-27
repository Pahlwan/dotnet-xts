using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTS_Access.Requests
{
    public class ClientConfigRequest
    {
        private string PathFromBaseURI { get; set; } = "/config/clientConfig";
        public string URI { get; }
        public ClientConfigRequest(string baseURI)
        {
            URI = baseURI + PathFromBaseURI;
        }
    }
}
