using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTS_Access.Requests;
using XTS_Access.Response;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using XTS_Access.Domain;
using Quobject.SocketIoClientDotNet.Client;
using XTS_Access.LiveData;

namespace XTS_Access
{
    public delegate void delMarketDataFull(_1502PktFull pkt);
    public delegate void delTouchLineFull(_1501PktFull pkt);
    public delegate void delIndexDataFull(_1504PktFull pkt);
    public delegate void delCandleDataFull(_1505PktFull pkt);
    public delegate void delOpenInterestFull(_1510PktFull pkt);
    public class XTS
    {
        private static XTS _instance;
        private HttpClient _httpClient;
        private string _userId;
        public string UserID { get { return _userId; } }
        private string _token;
        public string Token { get { return _token; } }

        #region Events
        public event delMarketDataFull MarketDataFullEvent;
        public event delTouchLineFull TouchLineEvent;
        public event delIndexDataFull IndexDataFullEvent;
        public event delCandleDataFull CandleDataFullEvent;
        public event delOpenInterestFull OpenInterestFullEvent;
        #endregion Events

        LoginResponse loginResponse;

        private XTS() 
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://xts.compositedge.com/marketdata");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Singolton for single object for whole project
        /// </summary>
        public static XTS Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new XTS();
                return _instance;
            }
        }

        /// <summary>
        /// Login to api with valid credentials.
        /// return "success" on successfull login and token for future request to api 
        /// </summary>
        /// <param name="secrateKey"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public async Task<string> Login(string secrateKey,string apiKey)
        {
            LoginRequest loginRequest = new LoginRequest(_httpClient.BaseAddress.AbsoluteUri,secrateKey, apiKey);
            using (HttpResponseMessage responseMessage = await _httpClient.PostAsync(loginRequest.URI, loginRequest.PayLoad))
            {
                try
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        loginResponse = JsonConvert.DeserializeObject<LoginResponse>(await responseMessage.Content.ReadAsStringAsync());
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(loginResponse.result.token.ToString());
                        _userId = loginResponse.result.userID;
                        _token = loginResponse.result.token;
                        return loginResponse.description;
                    }
                    else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new ArgumentException("Bad Request");
                    }
                    else
                    {
                        throw new ArgumentException("Can't Login");
                    }
                }
                catch(Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        /// <summary>
        /// Logout request from api.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Logout()
        {
            LogoutRequest logoutRequest = new LogoutRequest(_httpClient.BaseAddress.AbsoluteUri);
            using(HttpResponseMessage responseMessage = await _httpClient.DeleteAsync(logoutRequest.URI))
            {
                try
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        LogoutResponse logoutResponse = JsonConvert.DeserializeObject<LogoutResponse>(await responseMessage.Content.ReadAsStringAsync());
                        return logoutResponse.description;
                    }
                    else if(responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new ArgumentException("Bad Request");
                    }
                    else
                    {
                        throw new ArgumentException("Can't logout");
                    }
                }
                catch(Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        /// <summary>
        /// To get current loged in user configuration info from api.
        /// </summary>
        /// <returns></returns>
        public async Task<ClientConfigResponse> GetClientConfig()
        {
            ClientConfigRequest clientConfigRequest = new ClientConfigRequest(_httpClient.BaseAddress.AbsoluteUri);
            using (HttpResponseMessage responseMessage = await _httpClient.GetAsync(clientConfigRequest.URI))
            {
                try
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ClientConfigResponse clientConfigResponse = JsonConvert.DeserializeObject<ClientConfigResponse>(await responseMessage.Content.ReadAsStringAsync());
                        return clientConfigResponse;
                    }
                    else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new ArgumentException("Bad Request");
                    }
                    else
                    {
                        throw new ArgumentException("Can't get config details");
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        /// <summary>
        /// To Get Quote Data
        /// </summary>
        /// <param name="instruments"></param>
        /// <param name="xtsMessageCode"></param>
        /// <param name="publishFormat"></param>
        /// <returns></returns>
        public async Task<QuoteResponse> GetQuote(Instrument[]  instruments,int  xtsMessageCode,string publishFormat="JSON")
        {
            QuoteRequest quoteRequest = new QuoteRequest(_httpClient.BaseAddress.AbsoluteUri, instruments,xtsMessageCode);
            using (HttpResponseMessage responseMessage = await _httpClient.PostAsync(quoteRequest.URI, quoteRequest.PayLoad))
            {
                try
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                        QuoteResponse quoteResponse = JsonConvert.DeserializeObject<QuoteResponse>(await responseMessage.Content.ReadAsStringAsync());
                        
                        return quoteResponse;
                    }
                    else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new ArgumentException("Bad Request");
                    }
                    else
                    {
                        throw new ArgumentException("Can't get Quotes");
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        /// <summary>
        /// Subscribe instruments for live feed
        /// </summary>
        /// <param name="instruments"></param>
        /// <param name="xtsMessageCode"></param>
        /// <returns></returns>
        public async Task<QuoteResponse> Subscribe(Instrument[] instruments,int xtsMessageCode)
        {
            SubscribeRequest subscribeRequest = new SubscribeRequest(_httpClient.BaseAddress.AbsoluteUri, instruments, xtsMessageCode);
            using (HttpResponseMessage responseMessage = await _httpClient.PostAsync(subscribeRequest.URI,subscribeRequest.PayLoad))
            {
                try
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                        QuoteResponse subscriptionResponse = JsonConvert.DeserializeObject<QuoteResponse>(await responseMessage.Content.ReadAsStringAsync());

                        return subscriptionResponse;
                    }
                    else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new ArgumentException("Bad Request");
                    }
                    else
                    {
                        throw new ArgumentException("Can't get Quotes");
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        /// <summary>
        /// Unsubscribe subscribed instrument
        /// </summary>
        /// <param name="instruments"></param>
        /// <param name="xtsMessageCode"></param>
        /// <returns></returns>
        public async Task<UnsubscibeResponse> Unsubscribe(Instrument[] instruments, int xtsMessageCode)
        {
            SubscribeRequest subscribeRequest = new SubscribeRequest(_httpClient.BaseAddress.AbsoluteUri, instruments, xtsMessageCode);
            using (HttpResponseMessage responseMessage = await _httpClient.PutAsync(subscribeRequest.URI, subscribeRequest.PayLoad))
            {
                try
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        UnsubscibeResponse unsubscriptionResponse = JsonConvert.DeserializeObject<UnsubscibeResponse>(await responseMessage.Content.ReadAsStringAsync());

                        return unsubscriptionResponse;
                    }
                    else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new ArgumentException("Bad Request");
                    }
                    else
                    {
                        throw new ArgumentException("Can't get Quotes");
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        /// <summary>
        /// Can use to download master data on day start of app start
        /// </summary>
        /// <param name="exchangeSegmentsList"></param>
        /// <returns></returns>
        public async Task<MasterResponse> DownlaodMaster(string[] exchangeSegmentsList)
        {
            MasterRequest masterRequest = new MasterRequest(_httpClient.BaseAddress.AbsoluteUri, exchangeSegmentsList);
            using(HttpResponseMessage responseMessage = await _httpClient.PostAsync(masterRequest.URI,masterRequest.PayLoad))
            {
                try
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MasterResponse masterResponse = JsonConvert.DeserializeObject<MasterResponse>(await responseMessage.Content.ReadAsStringAsync());

                        return masterResponse;
                    }
                    else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new ArgumentException("Bad Request");
                    }
                    else
                    {
                        throw new ArgumentException("Can't get Quotes");
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public async Task<OHLCResponse> GetOHLC(int exchangeSegment,int exchangeInstrumentID, DateTime startTime,DateTime endTime,int candleInterval = 5)
        {
            OHLCRequest oHLCRequest = new OHLCRequest(_httpClient.BaseAddress.AbsoluteUri, exchangeSegment, exchangeInstrumentID, startTime, endTime, candleInterval);
            using (HttpResponseMessage responseMessage = await _httpClient.GetAsync(oHLCRequest.URI))
            {
                try
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        OHLCResponse oHLCResponse = JsonConvert.DeserializeObject<OHLCResponse>(await responseMessage.Content.ReadAsStringAsync());

                        return oHLCResponse;
                    }
                    else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new ArgumentException("Bad Request");
                    }
                    else
                    {
                        throw new ArgumentException("Can't get Quotes");
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }


        /// <summary>
        /// Live Market data Socket
        /// </summary>
        public void CreateMarketdataSocket()
        {
            Quobject.SocketIoClientDotNet.Client.IO.Options options = new Quobject.SocketIoClientDotNet.Client.IO.Options()
            {
                IgnoreServerCertificateValidation = true,
                Path = "/marketdata/socket.io",
                Query = new Dictionary<string, string>()
                    {
                        { "token", loginResponse.result.token },
                        { "userID", loginResponse.result.userID },
                        { "source", "WebAPI" },
                        { "publishFormat", "JSON" },
                        { "broadcastMode", "Full" }
                    }
            };

            var socket = IO.Socket("https://xts.compositedge.com", options);

            //subscribe to the base methods
            socket.On(Socket.EVENT_CONNECT, (data) =>
            {
                Console.WriteLine($"Connect {data}");
                //TODO- create an event for Connection
            });

            socket.On("joined", (data) =>
            {
                //TODO- create an event for Join
            });

            socket.On("success", (data) =>
            {
                //TODO- create an event for succesfull connection
            });

            socket.On("warning", (data) =>
            {
                //TODO- create an event for Warnings
            });

            socket.On("error", (data) =>
            {
                //TODO- create an event for error
            });

            socket.On("logout", (data) =>
            {
                //TODO- create an event for Logout
            });

            socket.On("disconnect", (data) =>
            {
                //TODO- create an event for desconnection
            });


            socket.On($"1501-JSON-Full", (data) =>
            {
                if(TouchLineEvent!=null)
                {
                    TouchLineEvent(JsonConvert.DeserializeObject<_1501PktFull>(data.ToString()));
                }
            });

            socket.On($"1502-json-full", (data) =>
            {
                Console.WriteLine(data);
                if (MarketDataFullEvent != null)
                {
                    MarketDataFullEvent(JsonConvert.DeserializeObject<_1502PktFull>(data.ToString()));
                }
            });

            socket.On($"1504-json-full", (data) =>
             {
                 if (IndexDataFullEvent != null)
                 {
                     IndexDataFullEvent(JsonConvert.DeserializeObject<_1504PktFull>(data.ToString()));
                 }
             });

            socket.On($"1505-json-full", (data) =>
            {
                if (CandleDataFullEvent != null)
                {
                    CandleDataFullEvent(JsonConvert.DeserializeObject<_1505PktFull>(data.ToString()));
                }
            });

            socket.On($"1510-json-full", (data) =>
            {
                if (OpenInterestFullEvent != null)
                {
                    OpenInterestFullEvent(JsonConvert.DeserializeObject<_1510PktFull>(data.ToString()));
                }
            });

        }
    }
}
