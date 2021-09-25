using System;
using System.Net.Http.Json;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTS_Access.Requests;
using XTS_Access.Response;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using XTS_Access.Domain;

namespace XTS_Access
{
    public class XTS
    {
        private static XTS _instance;
        private HttpClient _httpClient;
        public string UserID { get; set; }

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
                        LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(await responseMessage.Content.ReadAsStringAsync());
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(loginResponse.result.token.ToString());
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
    }
}
