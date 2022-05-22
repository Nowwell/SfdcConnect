/****************************************************************************
*
*   File name: SfdcRestApi.cs
*   Author: Sean Fife
*   Create date: 4/20/2016
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the SfdcRestApi class for Salesforce REST Api Connections
*
****************************************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SfdcConnect.SoapObjects;
using System.Web;
using System.Net.Sockets;

namespace SfdcConnect
{

    /// <summary>
    /// Salesforce REST Api connection class that uses OAuth to authenticate
    /// </summary>
    public class SfdcRestWithOAuthApi
    {
        public SfdcRestWithOAuthApi(string refreshToken = "")
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            state = ConnectionState.Closed;
            RefreshToken = refreshToken;

            Token = "";
            lastStatusCode = HttpStatusCode.OK;
        }
        public SfdcRestWithOAuthApi(string uri, string refreshToken = "")
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            state = ConnectionState.Closed;
            RefreshToken = refreshToken;

            this.Url = uri;
            ApiEndPoint = new Uri(this.Url);
            baseUrl = "https://" + ApiEndPoint.Host;
            Token = "";

            lastStatusCode = HttpStatusCode.OK;
        }
        public SfdcRestWithOAuthApi(bool isTest, int apiversion, string refreshToken = "")
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            state = ConnectionState.Closed;
            RefreshToken = refreshToken;

            if (isTest)
            {
                Url = string.Format("https://test.salesforce.com/services/Soap/u/{0}.0", apiversion);
            }
            else
            {
                Url = string.Format("https://login.salesforce.com/services/Soap/u/{0}.0", apiversion);
            }
            ApiEndPoint = new Uri(this.Url);
            baseUrl = "https://" + ApiEndPoint.Host;
            Token = "";

            Version = apiversion.ToString();

            lastStatusCode = HttpStatusCode.OK;
        }

        #region Variables
        /// <summary>
        /// Salesforce SessionId, only valid after Opening a connection. It is the same as the OAuth access token.
        /// </summary>
        public string SessionId { get; protected set; }
        /// <summary>
        /// OAuth Refresh Token
        /// </summary>
        public string RefreshToken { get; protected set; }
        /// <summary>
        /// Salesforce API Endpoint Uri, only valid after opening a connection.
        /// There is also Url which should contain the same value.
        /// </summary>
        protected string ServerUrl;
        /// <summary>
        /// Callback Endpoint for OAuth. If unset, it will generate an endpoint on http://127.0.0.1 for a random port.
        /// </summary>
        protected string CallbackEndpoint;
        /// <summary>
        /// State of the connection.  e.g. closed, open, connecting, etc.
        /// </summary>
        protected ConnectionState state;
        /// <summary>
        /// Date and time of the last Open or OpenAsync call
        /// </summary>
        protected DateTime LoginTime;
        /// <summary>
        /// Determines which OAuth flow is being used
        /// </summary>
        protected OAuthFlow Flow;

        /// <summary>
        /// OAuth ClientId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// OAuth ClientSecret
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// Username for the connection
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Plain text password for the connection
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Salseforce security token, if necessary
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// API Version, set after opening a connection. Or manually set
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// The connections current state
        /// </summary>
        public ConnectionState State
        {
            get { return state; }
        }

        /// <summary>
        /// Uri used for the REST API. Should be the same as ServerUrl
        /// </summary>
        public Uri ApiEndPoint { get; protected set; }


        string baseUrl = "";
        HttpStatusCode lastStatusCode;
        ApiLimits apiLimits = null;
        TcpListener oauthCallback;

        public string Url
        {
            get
            {
                return baseUrl;
            }
            set
            {
                baseUrl = value;
            }
        }
        #endregion

        /// <summary>
        /// Open a connection to Salesforce via the OAuth password flow.  Requires Username, Password, ClientId, and ClientSecret to be filled in.
        /// </summary>
        public string Open(OAuthFlow flowType = OAuthFlow.Password, string state = null)
        {
            Flow = flowType;
            if (flowType == OAuthFlow.Password)
            {
                OAuthPassword(state);
            }
            else
            {
                OAuthAuthorize(state);
            }
            return SessionId;
        }

        public void Close()
        {
            OAuthRequest request = new OAuthRequest();
            request.token = SessionId;

            string req = request.ToString();
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/services/oauth2/revoke", baseUrl));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(req);

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());

                    //SessionId = "";
                    this.state = ConnectionState.Closed;
                }
            }

            //SessionId = "";
            this.state = ConnectionState.Closed;
        }

        /// <summary>
        /// Initiates an OAuth Password flow
        /// </summary>
        /// <param name="state">State to be passed to the OAuth endpoint</param>
        /// <returns>token</returns>
        private string OAuthPassword(string state = null)
        {
            OAuthRequest request = new OAuthRequest();
            request.client_id = ClientId;
            request.client_secret = ClientSecret;
            request.username = Username;
            request.password = Password + Token;
            request.grant_type = "password";
            request.state = state;

            string req = request.ToString();
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/services/oauth2/token", baseUrl));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(req);

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());
                    this.state = ConnectionState.Open;
                    //TODO check signature, check that the response was good or bad


                    SessionId = returnValue.access_token;
                    ServerUrl = returnValue.instance_url;
                    ApiEndPoint = new Uri(returnValue.instance_url);
                    baseUrl = "https://" + ApiEndPoint.Host;
                    RefreshToken = returnValue.refresh_token;
                    this.state = ConnectionState.Open;
                    LoginTime = DateTime.Now;
                }
            }
            return SessionId;
        }

        /// <summary>
        /// Initiates an OAuth authorize flow
        /// </summary>
        /// <param name="originalstate">State to be passed to the OAuth endpoint</param>
        /// <returns>token</returns>
        private string OAuthAuthorize(string originalstate = null)
        {
            if (!string.IsNullOrEmpty(RefreshToken))
            {
                OAuthRefreshToken();
                return SessionId;
            }
            else
            {
                //then this call should work. and this should open a browser
                OAuthRequest request = new OAuthRequest();
                request.client_id = ClientId;
                request.client_secret = ClientSecret;
                if (string.IsNullOrEmpty(CallbackEndpoint))
                {
                    request.redirect_uri = string.Format("http://{0}:{1}/", IPAddress.Loopback.ToString(), FindAvailablePort());
                }
                else
                {
                    request.redirect_uri = CallbackEndpoint;
                }
                request.response_type = "code";
                request.state = originalstate;

                OAuthAuthorize(request);
                return SessionId;
            }
        }

        /// <summary>
        /// Initiates an OAuth authorize flow
        /// </summary>
        /// <param name="request">Class that contains the appropriate vlaues in the OAuthRequest parameter</param>
        /// <returns>token</returns>
        private string OAuthAuthorize(OAuthRequest request)
        {
            if(request.response_type != "code")
            {
                return "Invalid response type: " + request.response_type + ", should be 'code'";
            }

            if(!HttpListener.IsSupported)
            {
                return "HttpListener is not supported";
            }

            HttpListener server = new HttpListener();
            server.Prefixes.Add(request.redirect_uri);
            server.Start();

            //Open the browser to the Auth endpoint
            //TODO Make this process start async
            System.Diagnostics.Process.Start(string.Format("{0}/services/oauth2/authorize?{1}", baseUrl, request.ToString()));

            // Waits for the the user to go through and approve the app
            HttpListenerContext context = server.GetContext();

            HttpListenerResponse response = context.Response;
            string basicPage = "<html><head></head><body>{0}</body></html>";
            string responsePage = "";

            // get the code and save the state, if it exists
            string code = context.Request.QueryString.Get("code");
            string state = context.Request.QueryString.Get("state");

            responsePage = string.Format(basicPage, "You can close this browser and return to the app");
            // Checks for errors.
            string error = "";
            if (context.Request.QueryString.Get("error") != null)
            {
                responsePage = string.Format(basicPage, "There was an error with your request.");
                error = context.Request.QueryString.Get("error");
            }
            if (code == null || state == null)
            {
                responsePage = string.Format(basicPage, "We were unable to authorize you at this time.");
                error = "Error: Bad code or state";
            }
            if (state != request.state)
            {
                responsePage = string.Format(basicPage, "There was a problem with your request.");
                error = "Bad state";
            }

            //Show the page to the user and shut down the temporary http server
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responsePage);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Flush();
            response.Close();
            server.Stop();

            if(error!="")
            {
                this.state = ConnectionState.Closed;
                return error;
            }

            //do the code exchange: code -> token
            OAuthResponse resp = OAuthCodeToToken(code, request.redirect_uri);
            SessionId = resp.access_token;
            ServerUrl = resp.instance_url;
            ApiEndPoint = new Uri(resp.instance_url);
            baseUrl = "https://" + ApiEndPoint.Host;
            RefreshToken = resp.refresh_token;
            this.state = ConnectionState.Open;
            LoginTime = DateTime.Now;

            return SessionId;
        }

        /// <summary>
        /// Converts and OAuth code to a token. This is part of the OAuth Authorize flow
        /// </summary>
        /// <param name="code">Code from OAuth Authorize call</param>
        /// <param name="oauthCallback">Callback URL used from the OAuth Authorize call</param>
        /// <returns>OAuthResponse of the converted code to token</returns>
        private OAuthResponse OAuthCodeToToken(string code, string oauthCallback)
        {
            OAuthRequest request = new OAuthRequest();
            request.grant_type = "authorization_code";
            request.client_id = ClientId;
            request.code = code;
            request.redirect_uri = oauthCallback;

            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/services/oauth2/token", baseUrl));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());
                    
                    //TODO check signature

                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets a new access token from an OAuth refresh token
        /// </summary>
        /// <returns>OAuth refresh response</returns>
        private OAuthResponse OAuthRefreshToken()
        {
            OAuthRequest request = new OAuthRequest();
            request.grant_type = "refresh_token";
            request.client_id = ClientId;
            request.client_secret = ClientSecret;
            request.refresh_token = RefreshToken;

            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/services/oauth2/token", baseUrl));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());
                    this.state = ConnectionState.Open;
                    //TODO check signature, check that the response was good or bad
                    SessionId = returnValue.access_token;
                    ServerUrl = returnValue.instance_url;
                    ApiEndPoint = new Uri(returnValue.instance_url);
                    baseUrl = "https://" + ApiEndPoint.Host;
                    LoginTime = DateTime.Now;
                    //RefreshToken = returnValue.refresh_token;
                }
            }
            return returnValue;
        }

        private int FindAvailablePort()
        {
            //There has to be a better way to do this...
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 0);

            server.Start();
            
            int port = ((IPEndPoint)server.LocalEndpoint).Port;
            
            server.Stop();
            
            return port;
        }

        /// <summary>
        /// Gets a list of the available services for a Salesforce Org. Essentialy the API version numbers
        /// </summary>
        /// <param name="automaticallySetToMostRecentVersion">If true, will automatically set the Version parameter to be the most recent API version available</param>
        /// <returns>List of available services</returns>
        public List<SfRestServices> GetServices(bool automaticallySetToMostRecentVersion = true)
        {
            string url = baseUrl + string.Format("/services/data/");
            List<SfRestServices> returnValue = null;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/json";

            request.Headers.Add("Authorization: Bearer " + SessionId);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<List<SfRestServices>>(resp.ReadToEnd());
                    if(automaticallySetToMostRecentVersion) Version = returnValue[returnValue.Count - 1].version;
                }
            }

            return returnValue;
        }

        public static string ToSalesforceAPIHeaderDateTimeString(DateTime dt)
        {
            return dt.ToString("ddd, dd MMM yyyy HH:mm:ss z");
        }

        public object GetSObjects(DateTime ifModifiedSince)
        {

            string url = baseUrl + string.Format("/services/data/v{0}/sobjects/", Version);
            string header = "If-Modified-Since: " + ToSalesforceAPIHeaderDateTimeString(ifModifiedSince);//.ToString("ddd, dd MMM yyyy HH:mm:ss z");

            string whatsChanged = StandardAPICall("sobjects", "GET", string.Empty, string.Empty, "application/json", new string[] { header });

            if (lastStatusCode == HttpStatusCode.NotModified)
            {
                //nothing changed
                return null;
            }
            else
            {
                //something changed
                //deserialize response.
                return JsonConvert.DeserializeObject(whatsChanged);
            }
        }

        public object GetSObjects()
        {
            string url = baseUrl + string.Format("/services/data/v{0}/sobjects/", Version);

            string whatsChanged = StandardAPICall("sobjects", "GET", string.Empty, string.Empty, "application/json");

            if (lastStatusCode == HttpStatusCode.NotModified)
            {
                //nothing changed
                return null;
            }
            else
            {
                //something changed
                //deserialize response.
                return JsonConvert.DeserializeObject(whatsChanged);
            }
        }





        public string StandardAPICall(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
        {
            if (this.state != ConnectionState.Open)
            {
                return "Connection closed";
            }
            //should this endPoint just be the entire uri? or perhaps just the location?
            if (endPoint[0] != '/') endPoint = '/' + endPoint;
            string url = baseUrl + string.Format("/services/data/v{0}{1}", Version, endPoint);

            string returnValue = "";


            try
            {
                returnValue = Callout(url, method, package, contentType, accept, additionalHeaders);
            }
            catch(WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    this.state = ConnectionState.Closed;
                    if (Flow == OAuthFlow.Desktop)
                    {
                        OAuthRefreshToken();
                    }
                    else
                    {
                        Open(Flow);
                    }
                    if (this.state == ConnectionState.Open)
                    {
                        returnValue = Callout(url, method, package, contentType, accept, additionalHeaders);
                    }
                }
                else
                {
                    throw ex;
                }

            }
            return returnValue;
        }

        public T StandardAPICall<T>(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
        {
            return JsonConvert.DeserializeObject<T>(StandardAPICall(endPoint, method, package, contentType, accept, additionalHeaders));
        }

        public string CustomAPICall(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
        {
            if (this.state != ConnectionState.Open)
            {
                return "Connection closed";
            }
            if (endPoint[0] != '/') endPoint = '/' + endPoint;
            string url = baseUrl + string.Format("/services/apexrest/{0}", endPoint);
            string returnValue = "";

            try
            {
                returnValue = Callout(url, method, package, contentType, accept, additionalHeaders);
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    this.state = ConnectionState.Closed;
                    if (Flow == OAuthFlow.Desktop)
                    {
                        OAuthRefreshToken();
                    }
                    else
                    {
                        Open(Flow);
                    }
                    if (this.state == ConnectionState.Open)
                    {
                        returnValue = Callout(url, method, package, contentType, accept, additionalHeaders);
                    }
                }
                else
                {
                    throw ex;
                }
            }
            return returnValue;
        }

        public T CustomAPICall<T>(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
        {
            return JsonConvert.DeserializeObject<T>(CustomAPICall(endPoint, method, package, contentType, accept, additionalHeaders));
        }

        private string Callout(string endPoint, string method, string package, string contentType, string accept, string[] additionalHeaders = null)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(endPoint);
            request.Method = method;
            request.ContentType = contentType;
            request.Accept = accept;

            request.Headers.Add("Authorization: Bearer " + SessionId);


            if (additionalHeaders != null)
            {
                foreach (string s in additionalHeaders)
                {
                    request.Headers.Add(s);
                }
            }

            //everything but get can have data to send
            if (method.ToUpper() != "GET" && package.Trim().Length > 0)
            {
                byte[] buffer = null;
                buffer = System.Text.Encoding.UTF8.GetBytes(package);

                Stream postStream = request.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);
            }

            string returnValue = "";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                lastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = resp.ReadToEnd();
                }
            }

            return returnValue;
        }

    }
}
