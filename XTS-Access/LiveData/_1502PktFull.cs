using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTS_Access.Domain;

namespace XTS_Access.LiveData
{
    public class _1502PktFull
    {
        public int MessageCode;//1502,
        public int MessageVersion;//1,
        public int ApplicationType;//0,
        public int TokenID;//0,
        public int ExchangeSegment;//1,
        public int ExchangeInstrumentID;//22,
        public int ExchangeTimeStamp;//1205682251,
        public BidInfo[] Bids;
        public AskInfo[] Asks;
        public Touchline Touchline;
        public int BookType;//0,
        public int XMarketType;//0
        public long SequenceNumber;//186901854008294
    }
}
