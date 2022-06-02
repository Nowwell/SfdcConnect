/****************************************************************************
*
*   File name: SfdcLogin.cs
*   Author: Sean Fife
*   Create date: 5/27/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes the base class for Salesforce API Connections
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Web;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using SfdcConnect.OAuth;
using System.Text.Json;
using System.Reflection;
using System.Xml.Serialization;

namespace SfdcConnect
{
    /// <summary>
    /// Has login and logout functionality to obtain a session id for use general
    /// </summary>
    public enum Environment { Sandbox, Production };
    public class SfdcSession : System.ServiceModel.ClientBase<Sfdc.Soap.Partner.Soap>, ISfdcConnection
    {
        public SfdcSession(string refreshToken = "") :
                base(SfdcSoapApi.GetDefaultBinding(), new System.ServiceModel.EndpointAddress(string.IsNullOrEmpty(refreshToken) ? "https://login.salesforce.com/services/Soap/u/54.0" : "https://login.salesforce.com/services/oauth2/token"))
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            state = ConnectionState.Closed;

            LastStatusCode = HttpStatusCode.OK;
            DataProtector = new SfdcDataProtection();

            if (!string.IsNullOrEmpty(refreshToken))
            {
                LastRestLoginResponse = new Sfdc.Rest.loginResponse();
                LastRestLoginResponse.result = new Sfdc.Rest.LoginResult();
                LastRestLoginResponse.result.refresh_token = DataProtector.Encrypt(refreshToken);
            }
        }
        public SfdcSession(string uri, string refreshToken = "") :
                base(SfdcSoapApi.GetDefaultBinding(), new System.ServiceModel.EndpointAddress(uri))
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            state = ConnectionState.Closed;

            LastStatusCode = HttpStatusCode.OK;
            DataProtector = new SfdcDataProtection();

            if (!string.IsNullOrEmpty(refreshToken))
            {
                LastRestLoginResponse = new Sfdc.Rest.loginResponse();
                LastRestLoginResponse.result = new Sfdc.Rest.LoginResult();
                LastRestLoginResponse.result.refresh_token = DataProtector.Encrypt(refreshToken);
            }
        }
        public SfdcSession(Environment env, int apiversion, string refreshToken = "") :
                base(SfdcSoapApi.GetDefaultBinding(), new System.ServiceModel.EndpointAddress(string.Format("https://{0}.salesforce.com/services/Soap/u/{1}.0", env == Environment.Sandbox?"test":"login", apiversion)))
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            state = ConnectionState.Closed;

            LastStatusCode = HttpStatusCode.OK;
            DataProtector = new SfdcDataProtection();

            if (!string.IsNullOrEmpty(refreshToken))
            {
                LastRestLoginResponse = new Sfdc.Rest.loginResponse();
                LastRestLoginResponse.result = new Sfdc.Rest.LoginResult();
                LastRestLoginResponse.result.refresh_token = DataProtector.Encrypt(refreshToken);
            }
        }

        #region Variables

        internal SfdcDataProtection DataProtector;
        protected string sessionId;
        protected string refreshToken;

        protected Sfdc.Rest.loginRequest LastRestLoginRequest;
        protected Sfdc.Rest.loginResponse LastRestLoginResponse;

        protected Sfdc.Soap.Partner.loginRequest LastSoapLoginRequest;
        protected Sfdc.Soap.Partner.loginResponse LastSoapLoginResponse;

        public string SessionId
        {
            get { return DataProtector.Decrypt(sessionId); }
        }
        public string RefreshToken
        {
            get { return DataProtector.Decrypt(refreshToken); }
        }

        /// <summary>
        /// The connections current state
        /// </summary>
        public new ConnectionState State
        {
            get
            {
                if (Flow == LoginFlow.SOAP)
                {
                    return (ConnectionState)base.State;
                }
                else
                {
                    return state;
                }
            }
        }
        /// <summary>
        /// State of the connection.  e.g. closed, open, connecting, etc.
        /// </summary>
        protected ConnectionState state;
        /// <summary>
        /// Date and time of the last Open or OpenAsync call
        /// </summary>
        protected DateTime LoginTime;
        /// <summary>
        /// The last HTTP Status Code
        /// </summary>
        public System.Net.HttpStatusCode LastStatusCode { get; protected set; }
        /// <summary>
        /// Determines which OAuth flow is being used, or if SOAP is used
        /// </summary>
        public LoginFlow Flow { get; protected set; }

        #endregion

        #region Helpers
        private Sfdc.Soap.Partner.SessionHeader GetSessionHeader()
        {
            Sfdc.Soap.Partner.SessionHeader sessionHeader = new Sfdc.Soap.Partner.SessionHeader();
            sessionHeader.sessionId = DataProtector.Decrypt(sessionId);
            return sessionHeader;
        }

        private Sfdc.Soap.Partner.loginRequest RestToSoapRequest(Sfdc.Rest.loginRequest request)
        {
            Sfdc.Soap.Partner.loginRequest newRequest = new Sfdc.Soap.Partner.loginRequest();

            newRequest.CallOptions = new Sfdc.Soap.Partner.CallOptions();
            newRequest.LoginScopeHeader = new Sfdc.Soap.Partner.LoginScopeHeader();
            newRequest.username = request.username;
            newRequest.password = request.password;
            return newRequest;
        }

        private Sfdc.Rest.loginRequest SoapToRestRequest(Sfdc.Soap.Partner.loginRequest request)
        {
            Sfdc.Rest.loginRequest newRequest = new Sfdc.Rest.loginRequest();

            newRequest.username = request.username;
            newRequest.password = request.password;
            return newRequest;
        }
        /// <summary>
        /// Finds an open port on the local computer
        /// </summary>
        /// <returns>Port number</returns>
        private int FindAvailablePort()
        {
            //There has to be a better way to do this...
            System.Net.Sockets.TcpListener server = new System.Net.Sockets.TcpListener(IPAddress.Parse("127.0.0.1"), 0);

            server.Start();

            int port = ((IPEndPoint)server.LocalEndpoint).Port;

            server.Stop();

            return port;
        }

        /// <summary>
        /// Sets the Xml Serializer Flag. Fixes a bug in the way the serialization happens.
        /// </summary>
        /// <param name="flag">1 = On, 0 = Off</param>
        public static void SetXmlSerializerFlag(int flag = 1)
        {
            MethodInfo method = typeof(XmlSerializer).GetMethod("set_Mode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            method.Invoke(null, new object[] { flag });
        }
        #endregion

        #region Login Flows
        public void Open(LoginFlow flowType, Sfdc.Rest.loginRequest request)
        {
            if (this.state == ConnectionState.Open) return;

            this.state = ConnectionState.Connecting;
            request.Encrypt(DataProtector);
            Flow = flowType;
            if (flowType == LoginFlow.SOAP)
            {
                SetXmlSerializerFlag(1);
                OpenSoap(RestToSoapRequest(request));
                this.Endpoint.Address = new System.ServiceModel.EndpointAddress(LastSoapLoginResponse.result.serverUrl);
                sessionId = DataProtector.Encrypt(LastSoapLoginResponse.result.sessionId);
                refreshToken = null;
                SetXmlSerializerFlag(0);
            }
            else if (flowType == LoginFlow.OAuthUsernamePassword)
            {
                request = BuildUsernamePasswordRequest(request);
                LastRestLoginRequest = request;
                OAuthPassword(request);

                this.Endpoint.Address = new System.ServiceModel.EndpointAddress(LastRestLoginResponse.result.instance_url);
                sessionId = LastRestLoginResponse.result.access_token;
                refreshToken = LastRestLoginResponse.result.refresh_token;
            }
            else if (flowType == LoginFlow.WebServer)
            {
                request = BuildWebServerAuthorizeRequest(request);
                LastRestLoginRequest = request;
                OAuthAuthorize(request);

                this.Endpoint.Address = new System.ServiceModel.EndpointAddress(LastRestLoginResponse.result.instance_url);
                sessionId = LastRestLoginResponse.result.access_token;
                refreshToken = LastRestLoginResponse.result.refresh_token;
            }
            else if (flowType == LoginFlow.UserAgent)
            {
                request = BuildUserAgentRequest(request);
                LastRestLoginRequest = request;
                OAuthAuthorize(request);

                this.Endpoint.Address = new System.ServiceModel.EndpointAddress(LastRestLoginResponse.result.instance_url);
                sessionId = LastRestLoginResponse.result.access_token;
                refreshToken = LastRestLoginResponse.result.refresh_token;
            }
            else if (flowType == LoginFlow.JWTBearer)
            {
                //TODO Assertion part....
                string assertion = "";
                request = BuildJWTRequest(request, assertion);
                LastRestLoginRequest = request;
                JWTLogin(request);

                this.Endpoint.Address = new System.ServiceModel.EndpointAddress(LastRestLoginResponse.result.instance_url);
                sessionId = LastRestLoginResponse.result.access_token;
                refreshToken = LastRestLoginResponse.result.refresh_token;
            }
            else if (flowType == LoginFlow.AssetToken)
            {
                //TODO Agent token part...
                request = BuildAssetTokenRequest(request);
                LastRestLoginRequest = request;
                AssetFlow(request);

                this.Endpoint.Address = new System.ServiceModel.EndpointAddress(LastRestLoginResponse.result.instance_url);
                sessionId = LastRestLoginResponse.result.access_token;
                refreshToken = LastRestLoginResponse.result.refresh_token;
            }

            LoginTime = DateTime.Now;
        }

        /// <summary>
        /// Open a connection to Salesforce with the SOAP API.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public Sfdc.Soap.Partner.loginResponse OpenSoap(Sfdc.Soap.Partner.loginRequest request)
        {
            if (state == ConnectionState.Open) return LastSoapLoginResponse;
            state = ConnectionState.Connecting;

            Flow = LoginFlow.SOAP;

            request.username = DataProtector.Decrypt(request.username);
            request.password = DataProtector.Decrypt(request.password);
            LastSoapLoginResponse = Login(request);
            request.username = DataProtector.Encrypt(request.username);
            request.password = DataProtector.Encrypt(request.password);
            LastSoapLoginRequest = request;

            //if (EndpointsList.ContainsKey(EndpointConfiguration.LoggedIn))
            //{
            //    EndpointsList[EndpointConfiguration.LoggedIn] = LastSoapLoginResponse.result.serverUrl;
            //}
            //else
            //{
            //    EndpointsList.Add(EndpointConfiguration.LoggedIn, LastSoapLoginResponse.result.serverUrl);
            //}

            //ChangeEndpoint(EndpointConfiguration.LoggedIn);

            state = ConnectionState.Open;

            return LastSoapLoginResponse;
        }

        /// <summary>
        /// Initiates an OAuth Password flow
        /// </summary>
        /// <param name="state">State to be passed to the OAuth endpoint</param>
        /// <returns>token</returns>
        private void OAuthPassword(Sfdc.Rest.loginRequest request)
        {
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", this.Endpoint.Address.Uri.Scheme, this.Endpoint.Address.Uri.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            request.Decrypt(DataProtector);
            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());
            request.Encrypt(DataProtector);

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            Sfdc.Rest.loginResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonSerializer.Deserialize<Sfdc.Rest.loginResponse>(resp.ReadToEnd());
                    this.state = ConnectionState.Open;

                    LastRestLoginResponse = returnValue;
                    LastRestLoginResponse.result.Encrypt(DataProtector);
                }
            }
        }

        /// <summary>
        /// Initiates an OAuth authorize flow
        /// </summary>
        /// <param name="originalstate">State to be passed to the OAuth endpoint</param>
        /// <returns>token</returns>
        private void OAuthAuthorize(Sfdc.Rest.loginRequest request)
        {
            if (request != null && request.grant_type == "refresh_token")
            {
                OAuthRefreshToken(request);
            }
            else
            {
                if (request.response_type != "code")
                {
                    return;
                }

                if (!HttpListener.IsSupported)
                {
                    return;
                }

                HttpListener server = new HttpListener();
                server.Prefixes.Add(request.redirect_uri);
                server.Start();

                //Open the browser to the Auth endpoint
                //TODO Make this process start async
                request.Decrypt(DataProtector);
                System.Diagnostics.Process.Start(string.Format("{0}://{1}/services/oauth2/authorize?{2}", this.Endpoint.Address.Uri.Scheme, this.Endpoint.Address.Uri.Host, request.ToString()));
                request.Encrypt(DataProtector);

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

                if (error != "")
                {
                    this.state = ConnectionState.Closed;
                    return;
                }

                //do the code exchange: code -> token
                Sfdc.Rest.loginRequest tokenrequest = BuildWebServerCodeToTokenRequest(request, code);

                OAuthCodeToToken(tokenrequest);
                this.state = ConnectionState.Open;
            }
        }

        /// <summary>
        /// Converts and OAuth code to a token. This is part of the OAuth Authorize flow
        /// </summary>
        /// <param name="code">Code from OAuth Authorize call</param>
        /// <param name="oauthCallback">Callback URL used from the OAuth Authorize call</param>
        /// <returns>Sfdc.Rest.loginResponse of the converted code to token</returns>
        private Sfdc.Rest.loginResponse OAuthCodeToToken(Sfdc.Rest.loginRequest request)
        {
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", this.Endpoint.Address.Uri.Scheme, this.Endpoint.Address.Uri.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            request.Decrypt(DataProtector);
            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());
            request.Encrypt(DataProtector);

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            Sfdc.Rest.loginResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonSerializer.Deserialize<Sfdc.Rest.loginResponse>(resp.ReadToEnd());

                    LastRestLoginResponse = returnValue;
                    LastRestLoginResponse.result.Encrypt(DataProtector);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets a new access token from an OAuth refresh token
        /// </summary>
        /// <returns>OAuth refresh response</returns>
        private Sfdc.Rest.loginResponse OAuthRefreshToken(Sfdc.Rest.loginRequest request)
        {
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", this.Endpoint.Address.Uri.Scheme, this.Endpoint.Address.Uri.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            request.Decrypt(DataProtector);
            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());
            request.Encrypt(DataProtector);

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            Sfdc.Rest.loginResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonSerializer.Deserialize<Sfdc.Rest.loginResponse>(resp.ReadToEnd());
                    this.state = ConnectionState.Open;

                    LastRestLoginResponse = returnValue;
                    LastRestLoginResponse.result.Encrypt(DataProtector);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Sfdc.Rest.loginResponse JWTLogin(Sfdc.Rest.loginRequest request)
        {
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", this.Endpoint.Address.Uri.Scheme, this.Endpoint.Address.Uri.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";

            request.Decrypt(DataProtector);
            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());
            request.Encrypt(DataProtector);

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            Sfdc.Rest.loginResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonSerializer.Deserialize<Sfdc.Rest.loginResponse>(resp.ReadToEnd());

                    LastRestLoginResponse = returnValue;
                    LastRestLoginResponse.result.Encrypt(DataProtector);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Sfdc.Rest.loginResponse AssetFlow(Sfdc.Rest.loginRequest request)
        {
            if (request.response_type != "code")
            {
                return null;// "Invalid response type: " + request.response_type + ", should be 'code'";
            }

            if (!HttpListener.IsSupported)
            {
                return null;// "HttpListener is not supported";
            }

            HttpListener server = new HttpListener();
            server.Prefixes.Add(request.redirect_uri);
            server.Start();

            //Open the browser to the Auth endpoint
            //TODO Make this process start async
            request.Decrypt(DataProtector);
            System.Diagnostics.Process.Start(string.Format("{0}://{1}/services/oauth2/authorize?{2}", this.Endpoint.Address.Uri.Scheme, this.Endpoint.Address.Uri.Host, request.ToString()));
            request.Encrypt(DataProtector);

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

            if (error != "")
            {
                this.state = ConnectionState.Closed;
                return null;
            }

            //do the code exchange: code -> token
            Sfdc.Rest.loginRequest tokenrequest = BuildWebServerCodeToTokenRequest(request, code);

            Sfdc.Rest.loginResponse resp = OAuthCodeToToken(tokenrequest);

            Sfdc.Rest.loginRequest assetrequest = BuildAssetTokenRequest(tokenrequest, "");

            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", this.Endpoint.Address.Uri.Scheme, this.Endpoint.Address.Uri.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";

            buffer = Encoding.UTF8.GetBytes(assetrequest.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            Sfdc.Rest.loginResponse returnValue = null;
            using (HttpWebResponse httpresponse = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = httpresponse.StatusCode;
                using (StreamReader srresponse = new StreamReader(httpresponse.GetResponseStream()))
                {
                    returnValue = JsonSerializer.Deserialize<Sfdc.Rest.loginResponse>(srresponse.ReadToEnd());

                    LastRestLoginResponse = returnValue;
                    LastRestLoginResponse.result.Encrypt(DataProtector);
                    //TODO check signature
                    string AssetToken = returnValue.result.access_token;
                }
            }

            return returnValue;
        }
        #endregion

        public new void Close()
        {
            if (Flow == LoginFlow.SOAP)
            {
                //cannot logout with soap here... never opened this connection?


                //Sfdc.Soap.Partner.logoutRequest lr = new Sfdc.Soap.Partner.logoutRequest();
                //lr.SessionHeader = GetSessionHeader();
                //lr.CallOptions = new Sfdc.Soap.Partner.CallOptions();

                //Logout(lr);

                //state = ConnectionState.Closed;
            }
            else
            {
                Sfdc.Rest.loginRequest request = new Sfdc.Rest.loginRequest();
                request.token = "";

                string req = request.ToString();
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/revoke", this.Endpoint.Address.Uri.Scheme, this.Endpoint.Address.Uri.Host));
                webrequest.Method = "POST";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

                byte[] buffer = Encoding.UTF8.GetBytes(req);

                Stream postStream = webrequest.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);

                Sfdc.Rest.loginResponse returnValue = null;
                using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
                {
                    LastStatusCode = response.StatusCode;
                    using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                    {
                        returnValue = JsonSerializer.Deserialize<Sfdc.Rest.loginResponse>(resp.ReadToEnd());

                        this.state = ConnectionState.Closed;
                    }
                }

                this.state = ConnectionState.Closed;
            }
        }

        #region Request Builders
        public Sfdc.Rest.loginRequest BuildJWTRequest(Sfdc.Rest.loginRequest request, string assertion)
        {
            Sfdc.Rest.loginRequest newRequest = new Sfdc.Rest.loginRequest();
            newRequest.assertion = assertion;
            newRequest.grant_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:grant-type:jwt-bearer");
            return newRequest;
        }

        public Sfdc.Rest.loginRequest BuildUserAgentRequest(Sfdc.Rest.loginRequest request)
        {
            Sfdc.Rest.loginRequest newRequest = new Sfdc.Rest.loginRequest();
            newRequest.client_id = request.client_id;
            newRequest.client_secret = request.client_secret;
            newRequest.response_type = "token";
            newRequest.login_hint = "";
            newRequest.nonce = "mynonce";
            newRequest.display = "touch";

            newRequest.redirect_uri = request.redirect_uri;

            return newRequest;
        }

        public Sfdc.Rest.loginRequest BuildWebServerAuthorizeRequest(Sfdc.Rest.loginRequest request)
        {
            Sfdc.Rest.loginRequest newRequest = new Sfdc.Rest.loginRequest();
            newRequest.client_id = request.client_id;
            newRequest.client_secret = request.client_secret;
            newRequest.response_type = "code";

            newRequest.redirect_uri = request.redirect_uri;

            return newRequest;
        }

        public Sfdc.Rest.loginRequest BuildWebServerCodeToTokenRequest(Sfdc.Rest.loginRequest request, string code)
        {
            Sfdc.Rest.loginRequest newRequest = new Sfdc.Rest.loginRequest();
            newRequest.code = code;
            newRequest.grant_type = "authorization_code";
            newRequest.client_id = request.client_id;
            newRequest.client_secret = request.client_secret;

            newRequest.redirect_uri = request.redirect_uri;

            return newRequest;
        }

        public Sfdc.Rest.loginRequest BuildUsernamePasswordRequest(Sfdc.Rest.loginRequest request)
        {
            Sfdc.Rest.loginRequest newRequest = new Sfdc.Rest.loginRequest();
            newRequest.client_id = request.client_id;
            newRequest.client_secret = request.client_secret;
            newRequest.username = request.username;
            newRequest.password = request.password;
            newRequest.grant_type = "password";

            return newRequest;
        }

        public Sfdc.Rest.loginRequest BuildRefreshTokenRequest(Sfdc.Rest.loginRequest request)
        {
            Sfdc.Rest.loginRequest newRequest = new Sfdc.Rest.loginRequest();
            newRequest.grant_type = "refresh_token";
            newRequest.client_id = request.client_id;
            newRequest.client_secret = request.client_secret;
            newRequest.refresh_token = request.refresh_token;
            return newRequest;
        }

        public Sfdc.Rest.loginRequest BuildAssetTokenRequest(Sfdc.Rest.loginRequest request, string actorToken = null)
        {
            Sfdc.Rest.loginRequest newRequest = new Sfdc.Rest.loginRequest();
            newRequest.grant_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:grant-type:token-exchange");
            newRequest.subject_token_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:token-type:access_token");
            newRequest.subject_token = sessionId;
            if (!string.IsNullOrEmpty(actorToken))
            {
                newRequest.actor_token_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:token-type:jwt");
                newRequest.actor_token = actorToken;
            }
            return newRequest;
        }

        //public string GenerateJWTToken(SecurityKey key, string username, string audience)
        //{
        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        //    Claim c = new Claim("sub", username);

        //    JwtSecurityToken token = new JwtSecurityToken(ClientId, audience, new Claim[] { c }, null, DateTime.Now.AddSeconds(300), new SigningCredentials(key, SecurityAlgorithms.RsaSha256));

        //    return handler.WriteToken(token);
        //}

        //public string GenerateAssetToken(X509Certificate2 cert, string keyfile, string assetTokenName, string deviceId, string assetName, string accountId, string contactId, string serialNumber, string deviceKey)
        //{
        //    SecurityKey key = GetKeyFromFile(keyfile);

        //    RSACryptoServiceProvider publickey = cert.PublicKey.Key as RSACryptoServiceProvider;
        //    RSAParameters param = new RSAParameters();
        //    if (publickey != null)
        //    {
        //        param = publickey.ExportParameters(false);
        //    }


        //    SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
        //    //JsonWebKey jwk = JsonWebKeyConverter.ConvertFromSecurityKey(key);
        //    JWK jwk = new JWK();
        //    jwk.n = Convert.ToBase64String(param.Modulus);
        //    jwk.e = Convert.ToBase64String(param.Exponent);


        //    //Create the Token.

        //    JsonSerializerSettings jsonignorenulls = new JsonSerializerSettings()
        //    {
        //        NullValueHandling = NullValueHandling.Ignore
        //    };

        //    JwtHeader header = new JwtHeader();
        //    header.Add("alg", "RS256");
        //    header.Add("typ", "JWT");

        //    List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>
        //    {
        //        new System.Security.Claims.Claim("did", deviceId, ClaimValueTypes.String),
        //        new System.Security.Claims.Claim("Name", assetTokenName, ClaimValueTypes.String),
        //        new System.Security.Claims.Claim("cnf", "{\"jwk\": " + JsonConvert.SerializeObject(jwk,jsonignorenulls) + "}", JsonClaimValueTypes.Json),
        //        new System.Security.Claims.Claim("Asset", new Asset()
        //        {
        //            ContactId = contactId,
        //            AccountId = accountId,
        //            SerialNumber = serialNumber,
        //            Name = assetName
        //        }.ToString(), JsonClaimValueTypes.Json)
        //    };

        //    //JwtPayload payload = new JwtPayload(claims);
        //    JwtSecurityToken token = new JwtSecurityToken(null, null, claims, null, null, creds);
        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        //    return handler.WriteToken(token);
        //}

        //public bool IsJwtValid(string token, string keyfile, out SecurityToken validatedToken)
        //{
        //    SecurityKey key = GetKeyFromFile(keyfile);

        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        //    TokenValidationParameters validationParameterse = new TokenValidationParameters()
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = key,
        //        ValidAlgorithms = new string[] { SecurityAlgorithms.RsaSha256 },
        //        ValidIssuer = ClientId,
        //        ValidateAudience = false
        //    };

        //    ClaimsPrincipal cp = handler.ValidateToken(token, validationParameterse, out validatedToken);
        //    return validatedToken != null;
        //}

        //public bool IsJwtValid(string token, TokenValidationParameters validationParameterse, out SecurityToken validatedToken)
        //{
        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        //    ClaimsPrincipal cp = handler.ValidateToken(token, validationParameterse, out validatedToken);
        //    return validatedToken != null;
        //}

        ////https://stackoverflow.com/questions/949727/bouncycastle-rsaprivatekey-to-net-rsaprivatekey
        //private static byte[] ConvertRSAParametersField(BigInteger n, int size)
        //{
        //    byte[] bs = n.ToByteArrayUnsigned();
        //    if (bs.Length == size)
        //        return bs;
        //    if (bs.Length > size)
        //        throw new ArgumentException("Specified size too small", "size");
        //    byte[] padded = new byte[size];
        //    Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
        //    return padded;
        //}

        //private SecurityKey GetKeyFromFile(string keyfile)
        //{
        //    RsaPrivateCrtKeyParameters priv = null;

        //    using (var reader = File.OpenText(keyfile)) // file containing RSA PKCS1 private key
        //    {
        //        var o = new PemReader(reader).ReadObject();
        //        if (o is RsaPrivateCrtKeyParameters)
        //        {
        //            priv = (RsaPrivateCrtKeyParameters)o;
        //        }
        //        else if (o is AsymmetricCipherKeyPair)
        //        {
        //            priv = (RsaPrivateCrtKeyParameters)((AsymmetricCipherKeyPair)o).Private;
        //        }
        //    }

        //    if (priv == null)
        //    {
        //        throw new CryptoException("Unable to read the key");
        //    }

        //    RSAParameters param = new RSAParameters();
        //    param.Modulus = priv.Modulus.ToByteArrayUnsigned();
        //    param.Exponent = priv.PublicExponent.ToByteArrayUnsigned();
        //    param.P = priv.P.ToByteArrayUnsigned();
        //    param.Q = priv.Q.ToByteArrayUnsigned();
        //    param.D = ConvertRSAParametersField(priv.Exponent, param.Modulus.Length);
        //    param.DP = ConvertRSAParametersField(priv.DP, param.P.Length);
        //    param.DQ = ConvertRSAParametersField(priv.DQ, param.Q.Length);
        //    param.InverseQ = ConvertRSAParametersField(priv.QInv, param.Q.Length);

        //    RSA rsa = RSA.Create();
        //    rsa.ImportParameters(param);

        //    return new RsaSecurityKey(rsa);
        //}
        #endregion

        #region SOAP Methods

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Sfdc.Soap.Partner.loginResponse Login(Sfdc.Soap.Partner.loginRequest request)
        {
            return base.Channel.login(request);
        }

        //public Sfdc.Soap.Partner.LoginResult Login(Sfdc.Soap.Partner.LoginScopeHeader LoginScopeHeader, Sfdc.Soap.Partner.CallOptions CallOptions, string username, string password)
        //{
        //    Sfdc.Soap.Partner.loginRequest inValue = new Sfdc.Soap.Partner.loginRequest();
        //    inValue.LoginScopeHeader = LoginScopeHeader;
        //    inValue.CallOptions = CallOptions;
        //    inValue.username = username;
        //    inValue.password = password;
        //    Sfdc.Soap.Partner.loginResponse retVal = this.Login(inValue);
        //    return retVal.result;
        //}

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Sfdc.Soap.Partner.logoutResponse Logout(Sfdc.Soap.Partner.logoutRequest request)
        {
            return base.Channel.logout(request);
        }

        //public Sfdc.Soap.Partner.LimitInfo[] Logout(Sfdc.Soap.Partner.SessionHeader SessionHeader, Sfdc.Soap.Partner.CallOptions CallOptions)
        //{
        //    Sfdc.Soap.Partner.logoutRequest inValue = new Sfdc.Soap.Partner.logoutRequest();
        //    inValue.SessionHeader = SessionHeader;
        //    inValue.CallOptions = CallOptions;
        //    Sfdc.Soap.Partner.logoutResponse retVal = this.Logout(inValue);
        //    return retVal.LimitInfoHeader;
        //}
        #endregion


        //public void ChangeEndpoint(EndpointConfiguration endpointConfiguration)
        //{
        //    Endpoint.Address = new System.ServiceModel.EndpointAddress(EndpointsList[endpointConfiguration]);
        //    Endpoint.Name = endpointConfiguration.ToString();
        //}

        //public static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        //{
        //    //if ((endpointConfiguration == EndpointConfiguration.Soap))
        //    //{
        //    System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
        //    result.MaxBufferSize = int.MaxValue;
        //    result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
        //    result.MaxReceivedMessageSize = int.MaxValue;
        //    result.AllowCookies = true;
        //    result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
        //    return result;
        //    //}
        //    throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        //}

        //public static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        //{
        //    if (EndpointsList.ContainsKey(endpointConfiguration))
        //    {
        //        return new System.ServiceModel.EndpointAddress(EndpointsList[endpointConfiguration]);
        //    }
        //    throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        //}

        //public static System.ServiceModel.Channels.Binding GetDefaultBinding()
        //{
        //    return GetBindingForEndpoint(EndpointConfiguration.Soap);
        //}

        //public static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        //{
        //    return GetEndpointAddress(EndpointConfiguration.Soap);
        //}

        //public static System.Collections.Generic.Dictionary<EndpointConfiguration, string> EndpointsList = new System.Collections.Generic.Dictionary<EndpointConfiguration, string>();

        //public enum EndpointConfiguration
        //{

        //    Soap,
        //    LoggedIn
        //}
    }

    public class JWT
    {
        public string did;
        public string Name;
        public CNF cnf = new CNF();
        public Asset Asset;


    }

    public class Asset
    {
        public string AccountId;
        public string ContactId;
        public string SerialNumber;
        public string Name;
    }

    public class CNF
    {
        public string kty;
        public string kid;
        public string n;
        public string e;
    }
}



/// <summary>
/// Initiates an OAuth authorize flow
/// </summary>
/// <param name="request">Class that contains the appropriate vlaues in the Sfdc.Rest.loginRequest parameter</param>
/// <returns>token</returns>
//private string OAuthAuthorize(Sfdc.Rest.loginRequest request)
//{
//    if (request.response_type != "code")
//    {
//        return "Invalid response type: " + request.response_type + ", should be 'code'";
//    }

//    if (!HttpListener.IsSupported)
//    {
//        return "HttpListener is not supported";
//    }

//    HttpListener server = new HttpListener();
//    server.Prefixes.Add(request.redirect_uri);
//    server.Start();

//    //Open the browser to the Auth endpoint
//    //TODO Make this process start async
//    Uri endpoint = new Uri(Url);
//    System.Diagnostics.Process.Start(string.Format("{0}://{1}/services/oauth2/authorize?{2}", endpoint.Scheme, endpoint.Host, request.ToString()));

//    // Waits for the the user to go through and approve the app
//    HttpListenerContext context = server.GetContext();

//    HttpListenerResponse response = context.Response;
//    string basicPage = "<html><head></head><body>{0}</body></html>";
//    string responsePage = "";

//    // get the code and save the state, if it exists
//    string code = context.Request.QueryString.Get("code");
//    string state = context.Request.QueryString.Get("state");

//    responsePage = string.Format(basicPage, "You can close this browser and return to the app");
//    // Checks for errors.
//    string error = "";
//    if (context.Request.QueryString.Get("error") != null)
//    {
//        responsePage = string.Format(basicPage, "There was an error with your request.");
//        error = context.Request.QueryString.Get("error");
//    }
//    if (code == null || state == null)
//    {
//        responsePage = string.Format(basicPage, "We were unable to authorize you at this time.");
//        error = "Error: Bad code or state";
//    }
//    if (state != request.state)
//    {
//        responsePage = string.Format(basicPage, "There was a problem with your request.");
//        error = "Bad state";
//    }

//    //Show the page to the user and shut down the temporary http server
//    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responsePage);
//    response.ContentLength64 = buffer.Length;
//    response.OutputStream.Write(buffer, 0, buffer.Length);
//    response.OutputStream.Flush();
//    response.Close();
//    server.Stop();

//    if (error != "")
//    {
//        this.state = ConnectionState.Closed;
//        return error;
//    }

//    //do the code exchange: code -> token
//    Sfdc.Rest.loginResponse resp = OAuthCodeToToken(code, request.redirect_uri);
//    SessionId = resp.access_token;
//    ServerUrl = resp.instance_url;
//    ApiEndPoint = new Uri(resp.instance_url);
//    baseUrl = "https://" + ApiEndPoint.Host;
//    RefreshToken = resp.refresh_token;
//    identityEndpoint = resp.id;
//    this.state = ConnectionState.Open;
//    LoginTime = DateTime.Now;

//    return SessionId;
//}









//public Sfdc.Rest.loginRequest BuildJWTRequest(Sfdc.Rest.loginRequest request)
//{
//    request.grant_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:grant-type:jwt-bearer");
//    return request;
//}

//public Sfdc.Rest.loginRequest BuildUserAgentRequest(Sfdc.Rest.loginRequest request)
//{
//    request.response_type = "token";
//    request.login_hint = "";
//    request.nonce = "mynonce";
//    request.display = "touch";

//    if (string.IsNullOrEmpty(request.redirect_uri))
//    {
//        request.redirect_uri = string.Format("http://{0}:{1}/", IPAddress.Loopback.ToString(), FindAvailablePort());
//    }

//    return request;
//}

//public Sfdc.Rest.loginRequest BuildWebServerAuthorizeRequest(Sfdc.Rest.loginRequest request)
//{
//    request.response_type = "code";

//    if (string.IsNullOrEmpty(request.redirect_uri))
//    {
//        request.redirect_uri = string.Format("http://{0}:{1}/", IPAddress.Loopback.ToString(), FindAvailablePort());
//    }

//    return request;
//}

//public Sfdc.Rest.loginRequest BuildWebServerCodeToTokenRequest(Sfdc.Rest.loginRequest request, string code)
//{
//    request.code = code;
//    request.grant_type = "authorization_code";

//    if (string.IsNullOrEmpty(request.redirect_uri))
//    {
//        request.redirect_uri = string.Format("http://{0}:{1}/", IPAddress.Loopback.ToString(), FindAvailablePort());
//    }

//    return request;
//}

//public Sfdc.Rest.loginRequest BuildUsernamePasswordRequest(Sfdc.Rest.loginRequest request)
//{
//    request.grant_type = "password";

//    return request;
//}

//public Sfdc.Rest.loginRequest BuildRefreshTokenRequest(Sfdc.Rest.loginRequest request)
//{
//    request.grant_type = "refresh_token";
//    return request; 
//}

//public Sfdc.Rest.loginRequest BuildAssetTokenRequest(Sfdc.Rest.loginRequest request, string actorToken = null)
//{
//    request.grant_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:grant-type:token-exchange");
//    request.subject_token_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:token-type:access_token");
//    if (!string.IsNullOrEmpty(actorToken))
//    {
//        request.actor_token_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:token-type:jwt");
//        request.actor_token = actorToken;
//    }
//    return request;
//}