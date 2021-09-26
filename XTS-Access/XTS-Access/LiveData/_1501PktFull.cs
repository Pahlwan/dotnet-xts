using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTS_Access.Domain;

namespace XTS_Access.LiveData
{
    public class _1501PktFull
    {
        public int MessageCode;// 1501,
        public int ExchangeSegment;//:1,
        public int ExchangeInstrumentID; //22,
        public int ExchangeTimeStamp;//1205682251,
        public double LastTradedPrice;//1567.95,
        public int LastTradedQunatity;//20,
        public int TotalBuyQuantity; //1428,
        public int TotalSellQuantity;//0,
        public int TotalTradedQuantity;//253453,
        public int AverageTradedPrice;//1576.2,
        public int LastTradedTime;//1205682110,
        public int LastUpdateTime; //1205682251,
        public int PercentChange;//0,
        public double Open;//1599.9,
        public double High;//1607.25,
        public double Low;//1552.7,
        public double Close;//1567.95,
        public int TotalValueTraded;//177838490,
        public BidInfo BidInfo;
        public AskInfo AskInfo;
    }
}
