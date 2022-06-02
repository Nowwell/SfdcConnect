/****************************************************************************
*
*   File name: SfdcLogin.cs
*   Author: Sean Fife
*   Create date: 4/20/2016
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
using System.Web.Services.Protocols;
using SfdcConnect.SoapObjects;
using System.Security.Cryptography;
using SfdcConnect.Objects;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Web;

using System.IdentityModel.Tokens.Jwt;


using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace SfdcConnect
{
    /// <summary>
    /// Base connection class for all of the Salesforce API classes
    /// 
    /// Has login and logout functionality to obtain a session id for use in
    ///   the derived classes.
    /// </summary>

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "SoapBinding", Namespace = "urn:partner.soap.sforce.com")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiFault))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ChangeEventHeader))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NameObjectValuePair))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutFieldsDisplayed))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutButtonsDisplayed))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(location))]

    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Web.Services.WebServiceBindingAttribute(Name = "SoapBinding", Namespace = "urn:partner.soap.sforce.com")]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiFault))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(NameObjectValuePair))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutFieldsDisplayed))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutButtonsDisplayed))]
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(location))]
    public class SfdcConnection : SfdcLoginBase
    {
        public SfdcConnection() : base()
        {
            LastStatusCode = HttpStatusCode.OK;
        }

        public SfdcConnection(string refreshToken = "") : base()
        {
            RefreshToken = refreshToken;
            LastStatusCode = HttpStatusCode.OK;
        }
        public SfdcConnection(string uri, string refreshToken = "") : base(uri)
        {
            RefreshToken = refreshToken;
            LastStatusCode = HttpStatusCode.OK;
        }
        public SfdcConnection(bool isTest, int apiversion, string refreshToken = "") : base(isTest, apiversion)
        {
            RefreshToken = refreshToken;
            LastStatusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// Open a connection to Salesforce with the SOAP API.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public override void Open()
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;

            if (!string.IsNullOrEmpty(ServerUrl))
            {
                Url = ServerUrl;
            }

            Flow = Objects.LoginFlow.SOAP;
            LoginResult loginResult = Login(Username, Password + Token);

            LoginTime = DateTime.Now;
            Url = loginResult.serverUrl;
            SessionHeaderValue = new SessionHeader();
            SessionHeaderValue.sessionId = loginResult.sessionId;

            CallOptionsValue = new CallOptions();

            SessionId = loginResult.sessionId;
            ServerUrl = loginResult.serverUrl;

            string[] pieces = ServerUrl.Split('/');

            Version = pieces[pieces.Length - 2];

            ApiEndPoint = new Uri(loginResult.serverUrl);
            MetadataEndPoint = new Uri(loginResult.metadataServerUrl);

            baseUrl = "https://" + ApiEndPoint.Host;
            state = ConnectionState.Open;
        }

        /// <summary>
        /// Opens a Connection to Salesforce with the specified login flow.
        /// </summary>
        /// <param name="flowType">SOAP, OAuth Desktop, OAuth username and password</param>
        /// <param name="state">(optional) state parameter for OAuth flows</param>
        public void Open(Objects.LoginFlow flowType, OAuthRequest request)
        {
            if (this.state == ConnectionState.Open) return;

            this.state = ConnectionState.Connecting;
            Flow = flowType;
            if (flowType == Objects.LoginFlow.SOAP)
            {
                Open();
            }
            else if (flowType == Objects.LoginFlow.OAuthUsernamePassword)
            {
                OAuthPassword(request);
            }
            else if (flowType == LoginFlow.WebServer || flowType == LoginFlow.UserAgent)
            {
                OAuthAuthorize(request);
            }
            else if (flowType == LoginFlow.JWTBearer)
            {
                JWTLogin(request);
            }
            else if (flowType == LoginFlow.AssetToken)
            {
                AssetFlow(request);
            }

        }

        public void Open(Objects.LoginFlow flowType)
        {
            if (this.state == ConnectionState.Open) return;

            this.state = ConnectionState.Connecting;
            Flow = flowType;
            OAuthRequest request = null;
            if (flowType == Objects.LoginFlow.SOAP)
            {
                Open();
            }
            else if (flowType == Objects.LoginFlow.OAuthUsernamePassword)
            {
                request = BuildUsernamePasswordRequest();
                OAuthPassword(request);
            }
            else if (flowType == LoginFlow.WebServer)
            {
                request = BuildWebServerAuthorizeRequest();
                OAuthAuthorize(request);
            }
            else if (flowType == LoginFlow.UserAgent)
            {
                request = BuildUserAgentRequest();
                OAuthAuthorize(request);
            }
            else if (flowType == LoginFlow.JWTBearer)
            {
                //TODO Assertion part....
                request = BuildJWTRequest(AssetToken);
                JWTLogin(request);
            }
            else if (flowType == LoginFlow.AssetToken)
            {
                //TODO Agent token part...
                request = BuildAssetTokenRequest(null);
                AssetFlow(request);
            }

        }

        /// <summary>
        /// Open a connection to Salesforce asynchronously.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public virtual void OpenAsync()
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;
            Flow = Objects.LoginFlow.SOAP;

            loginCompleted += ls_loginCompleted;
            LoginAsync(Username, Password + Token);

            //return default(Task);
        }
        /// <summary>
        /// Open a connection to Salesforce asynchronously.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public virtual void OpenAsync(customLoginCompletedEventHandler loginCompleted)
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;
            Flow = Objects.LoginFlow.SOAP;

            customLoginCompleted += loginCompleted;
            loginCompleted += ls_loginCompleted;
            LoginAsync(Username, Password + Token);

            //return default(Task);
        }

        /// <summary>
        /// Open a connection to Salesforce asynchronously.  Requires Usernamd and Password to be filled in.
        /// </summary>
        public virtual async Task OpenAsync(CancellationToken cancellationToken)
        {
            if (state == ConnectionState.Open) return;
            state = ConnectionState.Connecting;
            Flow = Objects.LoginFlow.SOAP;

            loginCompleted += ls_loginCompleted;
            await LoginAsync(Username, Password + Token, cancellationToken);
        }

        /// <summary>
        /// Callback for OpenAsync calls
        /// </summary>
        /// <param name="sender">should be this object</param>
        /// <param name="e">Event Args holding the login information from Salesforce</param>
        private void ls_loginCompleted(object sender, loginCompletedEventArgs e)
        {

            LoginTime = DateTime.Now;
            Url = e.Result.serverUrl;
            SessionHeaderValue = new SessionHeader();
            SessionHeaderValue.sessionId = e.Result.sessionId;

            SessionId = e.Result.sessionId;
            ServerUrl = e.Result.serverUrl;

            string[] pieces = ServerUrl.Split('/');

            Version = pieces[pieces.Length - 2];

            ApiEndPoint = new Uri(e.Result.serverUrl);

            state = ConnectionState.Open;

            if (customLoginCompleted != null) customLoginCompleted(sender, e);
        }

        /// <summary>
        /// Closes the connection based on the Flow used
        /// </summary>
        public override void Close()
        {
            if (Flow == LoginFlow.SOAP)
            {
                Url = ServerUrl;

                Logout();

                state = ConnectionState.Closed;
            }
            else
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
                    LastStatusCode = response.StatusCode;
                    using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                    {
                        returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());

                        this.state = ConnectionState.Closed;
                    }
                }

                this.state = ConnectionState.Closed;
            }
        }

        /// <summary>
        /// Initiates an OAuth Password flow
        /// </summary>
        /// <param name="state">State to be passed to the OAuth endpoint</param>
        /// <returns>token</returns>
        private string OAuthPassword(OAuthRequest request)
        {
            string req = request.ToString();
            Uri endpoint = new Uri(Url);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", endpoint.Scheme, endpoint.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(req);

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = response.StatusCode;
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
                    identityEndpoint = returnValue.id;
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
        private string OAuthAuthorize(OAuthRequest request)
        {
            if (!string.IsNullOrEmpty(RefreshToken) || (request != null && request.grant_type == "refresh_token"))
            {
                request = BuildRefreshTokenRequest();
                OAuthRefreshToken(request);
                return SessionId;
            }
            else
            {
                if (request.response_type != "code")
                {
                    return "Invalid response type: " + request.response_type + ", should be 'code'";
                }

                if (!HttpListener.IsSupported)
                {
                    return "HttpListener is not supported";
                }

                HttpListener server = new HttpListener();
                server.Prefixes.Add(request.redirect_uri);
                server.Start();

                //Open the browser to the Auth endpoint
                //TODO Make this process start async
                Uri endpoint = new Uri(Url);
                System.Diagnostics.Process.Start(string.Format("{0}://{1}/services/oauth2/authorize?{2}", endpoint.Scheme, endpoint.Host, request.ToString()));

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
                if (code == null || (state == null && request.state != null))
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
                    return error;
                }

                //do the code exchange: code -> token
                OAuthRequest tokenrequest = BuildWebServerCodeToTokenRequest(code);

                OAuthResponse resp = OAuthCodeToToken(tokenrequest);

                SessionId = resp.access_token;
                ServerUrl = resp.instance_url;
                ApiEndPoint = new Uri(resp.instance_url);
                baseUrl = "https://" + ApiEndPoint.Host;
                RefreshToken = resp.refresh_token;
                identityEndpoint = resp.id;
                this.state = ConnectionState.Open;
                LoginTime = DateTime.Now;

                return SessionId;
            }
        }

        /// <summary>
        /// Initiates an OAuth authorize flow
        /// </summary>
        /// <param name="request">Class that contains the appropriate vlaues in the OAuthRequest parameter</param>
        /// <returns>token</returns>
        //private string OAuthAuthorize(OAuthRequest request)
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
        //    OAuthResponse resp = OAuthCodeToToken(code, request.redirect_uri);
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

        /// <summary>
        /// Converts and OAuth code to a token. This is part of the OAuth Authorize flow
        /// </summary>
        /// <param name="code">Code from OAuth Authorize call</param>
        /// <param name="oauthCallback">Callback URL used from the OAuth Authorize call</param>
        /// <returns>OAuthResponse of the converted code to token</returns>
        private OAuthResponse OAuthCodeToToken(OAuthRequest request)
        {
            Uri endpoint = new Uri(Url);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", endpoint.Scheme, endpoint.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = response.StatusCode;
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
        private OAuthResponse OAuthRefreshToken(OAuthRequest request)
        {
            Uri endpoint = new Uri(Url);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", endpoint.Scheme, endpoint.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";//text/html,application/xhtml+xml,application/xml,

            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());
                    this.state = ConnectionState.Open;
                    //TODO check signature, check that the response was good or bad
                    SessionId = returnValue.access_token;
                    ServerUrl = returnValue.instance_url;
                    ApiEndPoint = new Uri(returnValue.instance_url);
                    baseUrl = "https://" + ApiEndPoint.Host;
                    identityEndpoint = returnValue.id;
                    LoginTime = DateTime.Now;
                }
            }
            return returnValue;
        }

        private OAuthResponse JWTLogin(OAuthRequest request)
        {
            Uri endpoint = new Uri(Url);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", endpoint.Scheme, endpoint.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";

            byte[] buffer = Encoding.UTF8.GetBytes(request.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = response.StatusCode;
                using (StreamReader resp = new StreamReader(response.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(resp.ReadToEnd());

                    //TODO check signature
                    this.state = ConnectionState.Open;
                    SessionId = returnValue.access_token;
                    ServerUrl = returnValue.instance_url;
                    ApiEndPoint = new Uri(returnValue.instance_url);
                    baseUrl = "https://" + ApiEndPoint.Host;
                    identityEndpoint = returnValue.id;
                    LoginTime = DateTime.Now;
                }
            }
            return returnValue;
        }

        private string AssetFlow(OAuthRequest request)
        {
            if (request.response_type != "code")
            {
                return "Invalid response type: " + request.response_type + ", should be 'code'";
            }

            if (!HttpListener.IsSupported)
            {
                return "HttpListener is not supported";
            }

            HttpListener server = new HttpListener();
            server.Prefixes.Add(request.redirect_uri);
            server.Start();

            //Open the browser to the Auth endpoint
            //TODO Make this process start async
            Uri endpoint = new Uri(Url);
            System.Diagnostics.Process.Start(string.Format("{0}://{1}/services/oauth2/authorize?{2}", endpoint.Scheme, endpoint.Host, request.ToString()));

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
                return error;
            }

            //do the code exchange: code -> token
            OAuthRequest tokenrequest = BuildWebServerCodeToTokenRequest(code);

            OAuthResponse resp = OAuthCodeToToken(tokenrequest);

            SessionId = resp.access_token;
            ServerUrl = resp.instance_url;
            ApiEndPoint = new Uri(resp.instance_url);
            baseUrl = "https://" + ApiEndPoint.Host;
            RefreshToken = resp.refresh_token;
            identityEndpoint = resp.id;
            LoginTime = DateTime.Now;

            OAuthRequest assetrequest = BuildAssetTokenRequest("");

            endpoint = new Uri(Url);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}://{1}/services/oauth2/token", endpoint.Scheme, endpoint.Host));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Accept = "application/json";

            buffer = Encoding.UTF8.GetBytes(assetrequest.ToString());

            Stream postStream = webrequest.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);

            OAuthResponse returnValue = null;
            using (HttpWebResponse httpresponse = (HttpWebResponse)webrequest.GetResponse())
            {
                LastStatusCode = httpresponse.StatusCode;
                using (StreamReader srresponse = new StreamReader(httpresponse.GetResponseStream()))
                {
                    returnValue = JsonConvert.DeserializeObject<OAuthResponse>(srresponse.ReadToEnd());

                    //TODO check signature
                    this.state = ConnectionState.Open;
                    AssetToken = returnValue.access_token;
                    LoginTime = DateTime.Now;
                }
            }

            return SessionId;

        }

        public OAuthRequest BuildJWTRequest(string assertion)
        {
            OAuthRequest request = new OAuthRequest();
            request.assertion = assertion;
            request.grant_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:grant-type:jwt-bearer");
            return request;
        }

        public OAuthRequest BuildUserAgentRequest()
        {
            OAuthRequest request = new OAuthRequest();
            request.client_id = ClientId;
            request.client_secret = ClientSecret;
            request.response_type = "token";
            request.login_hint = "";
            request.nonce = "mynonce";
            request.display = "touch";

            if (string.IsNullOrEmpty(CallbackEndpoint))
            {
                request.redirect_uri = string.Format("http://{0}:{1}/", IPAddress.Loopback.ToString(), FindAvailablePort());
            }
            else
            {
                request.redirect_uri = CallbackEndpoint;
            }

            return request;
        }

        public OAuthRequest BuildWebServerAuthorizeRequest()
        {
            OAuthRequest request = new OAuthRequest();
            request.client_id = ClientId;
            request.client_secret = ClientSecret;
            request.response_type = "code";

            if (string.IsNullOrEmpty(CallbackEndpoint))
            {
                request.redirect_uri = string.Format("http://{0}:{1}/", IPAddress.Loopback.ToString(), FindAvailablePort());
            }
            else
            {
                request.redirect_uri = CallbackEndpoint;
            }

            return request;
        }

        public OAuthRequest BuildWebServerCodeToTokenRequest(string code)
        {
            OAuthRequest request = new OAuthRequest();
            request.code = code;
            request.grant_type = "authorization_code";
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

            return request;
        }

        public OAuthRequest BuildUsernamePasswordRequest()
        {
            OAuthRequest request = new OAuthRequest();
            request.client_id = ClientId;
            request.client_secret = ClientSecret;
            request.username = Username;
            request.password = Password + Token;
            request.grant_type = "password";

            return request;
        }

        public OAuthRequest BuildRefreshTokenRequest()
        {
            OAuthRequest request = new OAuthRequest();
            request.grant_type = "refresh_token";
            request.client_id = ClientId;
            request.client_secret = ClientSecret;
            request.refresh_token = RefreshToken;
            return request;
        }

        public OAuthRequest BuildAssetTokenRequest(string actorToken = null)
        {
            OAuthRequest request = new OAuthRequest();
            request.grant_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:grant-type:token-exchange");
            request.subject_token_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:token-type:access_token");
            request.subject_token = SessionId;
            if (!string.IsNullOrEmpty(actorToken))
            {
                request.actor_token_type = HttpUtility.UrlEncode("urn:ietf:params:oauth:token-type:jwt");
                request.actor_token = actorToken;
            }
            return request;
        }

        /// <summary>
        /// Generate a JWT token for the JWT flow
        /// </summary>
        /// <param name="username">Username of the user to authenticate</param>
        /// <param name="audience">https://login.salesforce.com or https://test.salesforce.com</param>
        /// <param name="keyfile">RSA private key file used for signing</param>
        /// <returns>Signed JWT token</returns>
        public string GenerateJWTToken(string keyfile, string username, string audience)
        {
            SecurityKey key = GetKeyFromFile(keyfile);

            return GenerateJWTToken(key, username, audience);
        }

        public string GenerateJWTToken(SecurityKey key, string username, string audience)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            Claim c = new Claim("sub", username);

            JwtSecurityToken token = new JwtSecurityToken(ClientId, audience, new Claim[] { c }, null, DateTime.Now.AddSeconds(300), new SigningCredentials(key, SecurityAlgorithms.RsaSha256));

            return handler.WriteToken(token);
        }

        public string GenerateAssetToken(X509Certificate2 cert, string keyfile, string assetTokenName, string deviceId, string assetName, string accountId, string contactId, string serialNumber, string deviceKey)
        {
            SecurityKey key = GetKeyFromFile(keyfile);

            RSACryptoServiceProvider publickey = cert.PublicKey.Key as RSACryptoServiceProvider;
            RSAParameters param = new RSAParameters();
            if (publickey != null)
            {
                param = publickey.ExportParameters(false);
            }


            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            //JsonWebKey jwk = JsonWebKeyConverter.ConvertFromSecurityKey(key);
            JWK jwk = new JWK();
            jwk.n = Convert.ToBase64String(param.Modulus);
            jwk.e = Convert.ToBase64String(param.Exponent);


            //Create the Token.

            JsonSerializerSettings jsonignorenulls = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            JwtHeader header = new JwtHeader();
            header.Add("alg", "RS256");
            header.Add("typ", "JWT");

            List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim("did", deviceId, ClaimValueTypes.String),
                new System.Security.Claims.Claim("Name", assetTokenName, ClaimValueTypes.String),
                new System.Security.Claims.Claim("cnf", "{\"jwk\": " + JsonConvert.SerializeObject(jwk,jsonignorenulls) + "}", JsonClaimValueTypes.Json),
                new System.Security.Claims.Claim("Asset", new Asset()
                {
                    ContactId = contactId,
                    AccountId = accountId,
                    SerialNumber = serialNumber,
                    Name = assetName
                }.ToString(), JsonClaimValueTypes.Json)
            };

            //JwtPayload payload = new JwtPayload(claims);
            JwtSecurityToken token = new JwtSecurityToken(null, null, claims, null, null, creds);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        public bool IsJwtValid(string token, string keyfile, out SecurityToken validatedToken)
        {
            SecurityKey key = GetKeyFromFile(keyfile);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameterse = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidAlgorithms = new string[] { SecurityAlgorithms.RsaSha256 },
                ValidIssuer = ClientId,
                ValidateAudience = false
            };

            ClaimsPrincipal cp = handler.ValidateToken(token, validationParameterse, out validatedToken);
            return validatedToken != null;
        }

        public bool IsJwtValid(string token, TokenValidationParameters validationParameterse, out SecurityToken validatedToken)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            ClaimsPrincipal cp = handler.ValidateToken(token, validationParameterse, out validatedToken);
            return validatedToken != null;
        }

        //https://stackoverflow.com/questions/949727/bouncycastle-rsaprivatekey-to-net-rsaprivatekey
        private static byte[] ConvertRSAParametersField(BigInteger n, int size)
        {
            byte[] bs = n.ToByteArrayUnsigned();
            if (bs.Length == size)
                return bs;
            if (bs.Length > size)
                throw new ArgumentException("Specified size too small", "size");
            byte[] padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
            return padded;
        }

        private SecurityKey GetKeyFromFile(string keyfile)
        {
            RsaPrivateCrtKeyParameters priv = null;

            using (var reader = File.OpenText(keyfile)) // file containing RSA PKCS1 private key
            {
                var o = new PemReader(reader).ReadObject();
                if (o is RsaPrivateCrtKeyParameters)
                {
                    priv = (RsaPrivateCrtKeyParameters)o;
                }
                else if (o is AsymmetricCipherKeyPair)
                {
                    priv = (RsaPrivateCrtKeyParameters)((AsymmetricCipherKeyPair)o).Private;
                }
            }

            if (priv == null)
            {
                throw new CryptoException("Unable to read the key");
            }

            RSAParameters param = new RSAParameters();
            param.Modulus = priv.Modulus.ToByteArrayUnsigned();
            param.Exponent = priv.PublicExponent.ToByteArrayUnsigned();
            param.P = priv.P.ToByteArrayUnsigned();
            param.Q = priv.Q.ToByteArrayUnsigned();
            param.D = ConvertRSAParametersField(priv.Exponent, param.Modulus.Length);
            param.DP = ConvertRSAParametersField(priv.DP, param.P.Length);
            param.DQ = ConvertRSAParametersField(priv.DQ, param.Q.Length);
            param.InverseQ = ConvertRSAParametersField(priv.QInv, param.Q.Length);

            RSA rsa = RSA.Create();
            rsa.ImportParameters(param);

            return new RsaSecurityKey(rsa);
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
        /// Returns the Limit Info returned with the SOAP login call
        /// </summary>
        public LimitInfo[] LimitInfoFromLogin
        {
            get
            {
                if (LimitInfoHeaderValue == null) return null;
                return LimitInfoHeaderValue.limitInfo;
            }
        }

        #region SFDC - Originally from auto generated wsdl
        public LoginScopeHeader LoginScopeHeaderValue { get; set; }
        public CallOptions CallOptionsValue { get; set; }
        public SessionHeader SessionHeaderValue { get; set; }
        public LimitInfoHeader LimitInfoHeaderValue { get; set; }

        public delegate void customLoginCompletedEventHandler(object sender, loginCompletedEventArgs e);
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
        public delegate void loginCompletedEventHandler(object sender, loginCompletedEventArgs e);
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
        public delegate void logoutCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);

        private SendOrPostCallback loginOperationCompleted;
        private SendOrPostCallback logoutOperationCompleted;

        public event loginCompletedEventHandler loginCompleted;
        public event customLoginCompletedEventHandler customLoginCompleted;
        public event logoutCompletedEventHandler logoutCompleted;

        [SoapHeaderAttribute("LoginScopeHeaderValue")]
        [SoapHeaderAttribute("CallOptionsValue")]
        [SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public LoginResult Login(string username, string password)
        {
            object[] results = this.Invoke("login", new object[] { username, password });
            return ((LoginResult)(results[0]));
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("CallOptionsValue")]
        [System.Web.Services.Protocols.SoapHeaderAttribute("LimitInfoHeaderValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapHeaderAttribute("SessionHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Logout()
        {
            this.Invoke("logout", new object[0]);
        }

        /// <remarks/>
        public void LoginAsync(string username, string password)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            this.LoginAsync(username, password, null);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
        /// <remarks/>
        public async Task LoginAsync(string username, string password, object userState)
        {
            if ((this.loginOperationCompleted == null))
            {
                this.loginOperationCompleted = new SendOrPostCallback(this.OnloginOperationCompleted);
            }
            await Task.Run(() =>
            {
                base.InvokeAsync("login", new object[] {
                        username,
                        password}, this.loginOperationCompleted, userState);
            });
        }
        private void OnloginOperationCompleted(object arg)
        {
            if ((this.loginCompleted != null))
            {
                InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
                this.loginCompleted(this, new loginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public void LogoutAsync()
        {
            this.LogoutAsync(null);
        }
        /// <remarks/>
        public void LogoutAsync(object userState)
        {
            if ((this.logoutOperationCompleted == null))
            {
                this.logoutOperationCompleted = new SendOrPostCallback(this.OnlogoutOperationCompleted);
            }
            base.InvokeAsync("logout", new object[0], this.logoutOperationCompleted, userState);
        }
        private void OnlogoutOperationCompleted(object arg)
        {
            if ((this.logoutCompleted != null))
            {
                InvokeCompletedEventArgs invokeArgs = ((InvokeCompletedEventArgs)(arg));
                this.logoutCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        #endregion
    }

    public class JWK
    {
        public string kty = "RSA";
        public string n;
        public string e;
        public string use = "sig";
    }

    public class Asset
    {
        public string AccountId;
        public string ContactId;
        public string SerialNumber;
        public string Name;

        public override string ToString()
        {
            JsonSerializerSettings jsonignorenulls = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(this, jsonignorenulls);
        }
    }
}
