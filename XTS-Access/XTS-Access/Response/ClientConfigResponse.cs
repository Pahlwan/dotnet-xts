using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTS_Access.Response
{
    public class ConfigResponseResult
    {
        public Dictionary<string, string> exchangeSegments;
        public Dictionary<string, string> xtsmessageCode;
        public string[] publishFormat;
        public string[] broadCastMode;
        public Dictionary<string, string> instrumentType;
    }
    public class ClientConfigResponse
    {
        public string type;// success
        public string code;//s-response-0001
        public string description; //Fetched configurations successfully"
        public ConfigResponseResult result;
    }
}
