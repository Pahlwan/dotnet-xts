using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTS_Access.Domain
{
    public enum ExchangeSegment
    {
        NSECM = 1,
        NSEFO = 2,
        NSECD = 3,
        NSECO = 4,
        SLBM = 5,
        BSECM = 11,
        BSEFO = 12,
        BSECD = 13,
        BSECO = 14,
        NCDEX = 21,
        MSECM = 41,
        MSEFO = 42,
        MSECD = 43,
        MCXFO = 51
    }

    public enum XTSMessageCode
    {
        Touchline = 1501,
        MarketData = 1502,
        IndexData = 1504,
        CandleData = 1505,
        OpenInterest = 1510
    }

    public enum Series
    {
        FUTIDX = 0,
        OPTIDX = 1,
        FUTSTK = 2,
        OPTSTK = 3
    }

    public enum PublishFormat
    {
        Binary = 0,
        Json = 1
    }

    public enum BroadcastMode
    {
        Full = 0,
        partial = 1
    }

    public enum InstrumentType
    {
        Futures = 1,
        Options = 2,
        Spread = 4,
        Equity = 8,
        Spot = 16,
        PreferenceShares = 32,
        Debentures = 64,
        Warrants = 128,
        Miscellaneous = 256,
        MutualFund = 512
    }
    public enum OptionType
    {
        CE = 3,
        PE = 4
    }
}
