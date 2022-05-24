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

namespace SfdcConnect
{
    /// <summary>
    /// Base connection class for all of the Salesforce API classes
    /// 
    /// Has login and logout functionality to obtain a session id for use in
    ///   the derived classes.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "SoapBinding", Namespace = "urn:partner.soap.sforce.com")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiFault))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(NameObjectValuePair))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutFieldsDisplayed))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SearchLayoutButtonsDisplayed))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(location))]
    public class SfdcConnection : SfdcLoginBase
    {
        public SfdcConnection(string refreshToken = "") : base()
        {
            RefreshToken = refreshToken;
            lastStatusCode = HttpStatusCode.OK;
        }
        public SfdcConnection(string uri, string refreshToken = "") : base(uri)
        {
            RefreshToken = refreshToken;
            lastStatusCode = HttpStatusCode.OK;
        }
        public SfdcConnection(bool isTest, int apiversion, string refreshToken = "") : base(isTest, apiversion)
        {
            RefreshToken = refreshToken;
            lastStatusCode = HttpStatusCode.OK;
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
            LoginResult loginResult = login(Username, Password + Token);

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
        public void Open(Objects.LoginFlow flowType, string state = null)
        {
            Flow = flowType;
            if (flowType == Objects.LoginFlow.SOAP)
            {
                Open();
                
            }
            else if (flowType == Objects.LoginFlow.OAuthPassword)
            {
                OAuthPassword(state);
            }
            else
            {
                OAuthAuthorize(state);
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

                logout();

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
                    lastStatusCode = response.StatusCode;
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
            if (request.response_type != "code")
            {
                return "Invalid response type: " + request.response_type + ", should be 'code'";
            }

            if (!HttpListener.IsSupported)
            {
                return "HttpListener is not supported";
            }

            try
            {
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
                OAuthResponse resp = OAuthCodeToToken(code, request.redirect_uri);
                SessionId = resp.access_token;
                ServerUrl = resp.instance_url;
                ApiEndPoint = new Uri(resp.instance_url);
                baseUrl = "https://" + ApiEndPoint.Host;
                RefreshToken = resp.refresh_token;
                this.state = ConnectionState.Open;
                LoginTime = DateTime.Now;
            }
            catch(Exception e)
            {
                string error = e.Message;
            }
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
                }
            }
            return returnValue;
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
        public LoginResult login(string username, string password)
        {
            object[] results = this.Invoke("login", new object[] { username, password });
            return ((LoginResult)(results[0]));
        }

        [SoapHeaderAttribute("SessionHeaderValue")]
        [SoapHeaderAttribute("CallOptionsValue")]
        [SoapHeaderAttribute("LimitInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
        [SoapDocumentMethodAttribute("", RequestNamespace = "urn:partner.soap.sforce.com", ResponseNamespace = "urn:partner.soap.sforce.com", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public void logout()
        {
            this.Invoke("logout", new object[0]);
        }

        /// <remarks/>
        public void LoginAsync(string username, string password)
        {
            this.LoginAsync(username, password, null);
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
}
