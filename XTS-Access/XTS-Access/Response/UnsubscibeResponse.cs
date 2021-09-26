using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTS_Access.Domain;

namespace XTS_Access.Response
{
    public class UnsubscribeResult
    {
        public int mdp;// 1502,
        public Instrument[] unsubList;// 
    }

    public class UnsubscibeResponse
    {
        public string type;// "success",
        public string code;// "s-response-0001",
        public string description;// "Instrument subscription deleted!",
        public UnsubscribeResult result;
    }
}
