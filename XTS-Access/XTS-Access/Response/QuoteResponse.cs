using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTS_Access.Domain;

namespace XTS_Access.Response
{
    public class QuoteResult
    {
        public int mdp;// 1502,
        public Instrument[] quotesList;// = new QuotePkt[10];
        public Quote[] listQuotes;
    }
    public class QuoteResponse
    {
        public string type;// "success",
        public string code;// "s-response-0001",
        public string description;// "Get quotes successfully!",
        public QuoteResult result;

        public QuoteResponse(int instrumentCount = 1,int marketDepthCount = 5)
        {
            
        }
    }
}
