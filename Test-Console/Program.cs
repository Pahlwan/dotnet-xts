using System;
using System.Threading.Tasks;
using XTS_Access;
using XTS_Access.Requests;
using XTS_Access.Response;
using XTS_Access.LiveData;
using XTS_Access.Domain;
using System.Collections.Generic;
using System.IO;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<string> loginTask =  XTS.Instance.Login("Tlpy045@M5", "e16c965046e51516c3e171");
            loginTask.Wait();
            Task<ClientConfigResponse> clientConfigTask = XTS.Instance.GetClientConfig();
            clientConfigTask.Wait();

            List<string> marketSegments = new List<string>();
            foreach(string marketSegment in clientConfigTask.Result.result.exchangeSegments.Keys)
            {
                if (marketSegment.Length > 2)
                {
                    marketSegments.Add(marketSegment);
                }
            }
            Task<MasterResponse> masterDownLoadTask = XTS.Instance.DownlaodMaster(marketSegments.ToArray());
            masterDownLoadTask.Wait();
            //File.WriteAllLines("Master.txt",masterDownLoadTask.Result.result.Split('\n'));
            Task<QuoteResponse> subscriptionTask = XTS.Instance.GetQuote(new Instrument[] { new Instrument(){exchangeInstrumentID = 2885, exchangeSegment = 1} }, 1502);
            
            XTS.Instance.CreateMarketdataSocket();

            Console.ReadKey();
        }
    }
}
